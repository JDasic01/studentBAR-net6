
namespace studentBARLibrary.DataAcess
{
    public interface IFacultyData
    {
        Task Createfaculty(FacultyModel faculty);
        Task<List<FacultyModel>> GetAllFaculties();
        Task UpdateFaculty(FacultyModel Faculty);
    }
}