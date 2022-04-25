using Microsoft.Extensions.Caching.Memory;

namespace studentBARLibrary.DataAcess;

public class MongoCourseData : ICourseData
{
    private readonly IDbConnection _db;
    private readonly IMemoryCache _cache;
    private readonly IUserData _userData;
    private readonly IMongoCollection<CourseModel> _courses;
    private const string CacheName = "CourseData";

    public MongoCourseData(IDbConnection db, IUserData userData, IMemoryCache cache)
    {
        _db = db;
        _userData = userData;
        _cache = cache;
        _courses = db.CourseCollection;
    }

    public async Task<List<CourseModel>> GetAllCourses()
    {
        var output = _cache.Get<List<CourseModel>>(CacheName);
        if (output is null)
        {
            var results = await _courses.FindAsync(s => s.Archived == false);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
        }
        return output;
    }

    public async Task UpdateCourse(CourseModel course)
    {
        await _courses.ReplaceOneAsync(s => s.Id == course.Id, course);
        _cache.Remove(CacheName);
    }

    public async Task CreateCourse(CourseModel course)
    {
        var client = _db.Client;
        using var session = await client.StartSessionAsync();

        session.StartTransaction();

        try
        {
            var db = client.GetDatabase(_db.DbName);
            var coursesInTransactions = db.GetCollection<CourseModel>(_db.CourseCollectionName);
            await coursesInTransactions.InsertOneAsync(course);

            var usersInTransactions = db.GetCollection<UserModel>(_db.UserCollectionName);
            var user = await _userData.GetUser(course.Id);
            user.AuthoredCourses.Add(new BasicCourseModel(course));
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
