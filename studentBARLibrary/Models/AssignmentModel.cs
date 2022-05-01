namespace studentBARLibrary.Models;

public class AssignmentModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public BasicUserModel Author { get; set; }
    public BasicCourseModel Course { get; set; }
    public bool Archived { get; set; } = false;
    public HashSet<string> UsersSaved { get; set; } = new();
}
