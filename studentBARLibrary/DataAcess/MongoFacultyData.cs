using Microsoft.Extensions.Caching.Memory;

namespace studentBARLibrary.DataAcess;

public class MongoFacultyData : IFacultyData
{
    private readonly IDbConnection _db;
    private readonly IMemoryCache _cache;
    private readonly IUserData _userData;
    private readonly IMongoCollection<FacultyModel> _faculties;
    private const string CacheName = "FacultyData";

    public MongoFacultyData(IDbConnection db, IUserData userData, IMemoryCache cache)
    {
        _db = db;
        _userData = userData;
        _cache = cache;
        _faculties = db.FacultyCollection;
    }

    public async Task<List<FacultyModel>> GetAllFaculties()
    {
        var output = _cache.Get<List<FacultyModel>>(CacheName);
        if (output is null)
        {
            var results = await _faculties.FindAsync(f => f.Archived == false);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
        }
        return output;
    }


    public async Task UpdateFaculty(FacultyModel Faculty)
    {
        await _faculties.ReplaceOneAsync(s => s.FacultyId == Faculty.FacultyId, Faculty);
        _cache.Remove(CacheName);
    }

    public async Task CreateFaculty(FacultyModel faculty)
    {
        var client = _db.Client;
        using var session = await client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            var db = client.GetDatabase(_db.DbName);
            var FacultysInTransactions = db.GetCollection<FacultyModel>(_db.FacultyCollectionName);
            await FacultysInTransactions.InsertOneAsync(faculty);

            var usersInTransactions = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(faculty.Author.Id);
            user.AuthoredFaculties.Add(new BasicFacultyModel(faculty));
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
