namespace studentBARLibrary.Models;

public class BasicCourseModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Course { get; set; }

    public BasicCourseModel()
    {

    }
    public BasicCourseModel(CourseModel course)
    {
        Id = course.Id;
        Course = course.CourseName;
    }
}
