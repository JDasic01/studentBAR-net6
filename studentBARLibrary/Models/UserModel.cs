namespace studentBARLibrary.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public string EmailAddress { get; set; }
    public List<BasicPostModel> AuthoredPosts { get; set; } = new();
    public List<BasicFacultyModel> AuthoredFaculties { get; set; } = new();
    public List<BasicUniversityModel> AuthoredUniversities { get; set; } = new();
    public List<BasicCourseModel> AuthoredCourses { get; set; } = new();
    public List<BasicPostModel> VotedOnPosts { get; set; } = new();
}
