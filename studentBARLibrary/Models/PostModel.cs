namespace studentBARLibrary.Models;

public class PostModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Post { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public UniversityModel University { get; set; }
    public FacultyModel Faculty { get; set; }
    public BasicUserModel Author { get; set; }
    public HashSet<string> UserVotes { get; set; } = new();
    public string OwnerNotes { get; set; }
    public bool Archived { get; set; } = false;
}
