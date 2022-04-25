using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace studentBARLibrary.DataAcess;

public class DbConnection : IDbConnection
{
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private string _connectionId = "MongoDB";
    public string DbName { get; private set; }
    public string UniversityCollectionName { get; private set; } = "universities";
    public string FacultyCollectionName { get; private set; } = "faculties";
    public string CourseCollectionName { get; private set; } = "courses";
    public string UserCollectionName { get; private set; } = "users";
    public string PostCollectionName { get; private set; } = "posts";

    public MongoClient Client { get; private set; }
    public IMongoCollection<UniversityModel> UniversityCollection { get; private set; }
    public IMongoCollection<PostModel> PostCollection { get; private set; }
    public IMongoCollection<UserModel> UserCollection { get; private set; }
    public IMongoCollection<FacultyModel> FacultyCollection { get; private set; }
    public IMongoCollection<CourseModel> CourseCollection { get; private set; }
    public DbConnection(IConfiguration config)
    {
        _config = config;
        Client = new MongoClient(_config.GetConnectionString(_connectionId));
        DbName = _config["DatabaseName"];
        _db = Client.GetDatabase(DbName);

        UniversityCollection = _db.GetCollection<UniversityModel>(UniversityCollectionName);
        PostCollection = _db.GetCollection<PostModel>(PostCollectionName);
        UserCollection = _db.GetCollection<UserModel>(UserCollectionName);
        FacultyCollection = _db.GetCollection<FacultyModel>(FacultyCollectionName);
        CourseCollection = _db.GetCollection<CourseModel>(CourseCollectionName);
    }
}
