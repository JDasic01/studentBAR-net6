namespace studentBARLibrary.Models;

public class BasicAssignmentModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Assignment { get; set; }
    public int Points { get; set; }

    public BasicAssignmentModel()
    {

    }
    public BasicAssignmentModel(AssignmentModel assignment)
    {
        Id = assignment.Id;
        Assignment = assignment.Name;
        Points = assignment.Points;
    }
}
