namespace studentBARLibrary.Models;

public class BasicFacultyModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Faculty { get; set; }

    public BasicFacultyModel()
    {

    }
    public BasicFacultyModel(FacultyModel faculty)
    {
        Id = faculty.FacultyId;
        Faculty = faculty.FacultyName;
    }
}
