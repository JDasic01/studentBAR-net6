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
    public List<CourseModel> Assignments { get; set; } = new();
    public bool Archived { get; set; } = false;
}
