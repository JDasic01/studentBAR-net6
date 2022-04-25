
namespace studentBARLibrary.DataAcess
{
    public interface ICourseData
    {
        Task CreateCourse(CourseModel course);
        Task<List<CourseModel>> GetAllCourses();
        Task UpdateCourse(CourseModel course);
    }
}