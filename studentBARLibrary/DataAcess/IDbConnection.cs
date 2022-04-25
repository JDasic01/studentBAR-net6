using MongoDB.Driver;

namespace studentBARLibrary.DataAcess
{
    public interface IDbConnection
    {
        MongoClient Client { get; }
        IMongoCollection<CourseModel> CourseCollection { get; }
        string CourseCollectionName { get; }
        string DbName { get; }
        IMongoCollection<FacultyModel> FacultyCollection { get; }
        string FacultyCollectionName { get; }
        IMongoCollection<PostModel> PostCollection { get; }
        string PostCollectionName { get; }
        IMongoCollection<UniversityModel> UniversityCollection { get; }
        string UniversityCollectionName { get; }
        IMongoCollection<UserModel> UserCollection { get; }
        string UserCollectionName { get; }
    }
}