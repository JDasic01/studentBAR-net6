using Microsoft.Extensions.Caching.Memory;

namespace studentBARLibrary.DataAcess;

public class MongoUniversityData : IUniversityData
{
    private readonly IDbConnection _db;
    private readonly IMemoryCache _cache;
    private readonly IUserData _userData;
    private readonly IMongoCollection<UniversityModel> _universities;
    private const string CacheName = "UniversityData";

    public MongoUniversityData(IDbConnection db, IUserData userData, IMemoryCache cache)
    {
        _db = db;
        _userData = userData;
        _cache = cache;
        _universities = db.UniversityCollection;
    }

    public async Task<List<UniversityModel>> GetAllSuggestions()
    {
        var output = _cache.Get<List<UniversityModel>>(CacheName);
        if (output is null)
        {
            var results = await _universities.FindAsync(s => s.Archived == false);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
        }
        return output;
    }


    public async Task UpdateSuggestion(UniversityModel university)
    {
        await _universities.ReplaceOneAsync(s => s.UniversityId == university.UniversityId, university);
        _cache.Remove(CacheName);
    }

    public async Task CreateSuggestion(UniversityModel university)
    {
        var client = _db.Client;
        using var session = await client.StartSessionAsync();

        session.StartTransaction();

        try
        {
            var db = client.GetDatabase(_db.DbName);
            var universitysInTransactions = db.GetCollection<UniversityModel>(_db.UniversityCollectionName);
            await universitysInTransactions.InsertOneAsync(university);

            var usersInTransactions = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(university.UniversityId);
            user.AuthoredUniversities.Add(new BasicUniversityModel(university));
            await usersInTransactions.ReplaceOneAsync(u => u.Id == user.Id, user);

            await session.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}
