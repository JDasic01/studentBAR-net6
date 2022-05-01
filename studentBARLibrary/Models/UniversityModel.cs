namespace studentBARLibrary.Models;

public class UniversityModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UniversityId { get; set; }
    public string UniversityName { get; set; }
    public string UniversityPlace { get; set; }
    public bool Archived { get; set; } = false;
    public BasicUserModel Author { get; set; }
    public List<BasicFacultyModel> Faculty { get; set; }
    public HashSet<string> UsersSaved { get; set; } = new();
}
