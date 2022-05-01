namespace studentBARLibrary.Models;

public class CourseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string CourseName { get; set; }
    public string CourseProfessor { get; set; }
    public string CourseAssistant { get; set; }
    public string CourseYear { get; set; }
    public List<AssignmentModel> Assignments { get; set; } = new();
    public bool Archived { get; set; } = false;
    public HashSet<string> UsersSaved { get; set; } = new();
}
