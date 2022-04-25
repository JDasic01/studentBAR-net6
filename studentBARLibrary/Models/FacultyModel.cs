namespace studentBARLibrary.Models;

public class FacultyModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string FacultyId { get; set; }
    public string FacultyName { get; set; }
    public bool Archived { get; set; } = false;
}
