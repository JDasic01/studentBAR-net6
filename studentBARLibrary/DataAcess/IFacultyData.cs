
namespace studentBARLibrary.DataAcess
{
    public interface IFacultyData
    {
        Task CreateFaculty(FacultyModel faculty);
        Task<List<FacultyModel>> GetAllFaculties();
        Task UpdateFaculty(FacultyModel Faculty);
    }
}