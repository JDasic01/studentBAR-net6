namespace studentBARLibrary.Models;

public class BasicUniversityModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string University { get; set; }

    public BasicUniversityModel()
    {

    }
    public BasicUniversityModel(UniversityModel university)
    {
        Id = university.UniversityId;
        University = university.UniversityName;
    }
}
