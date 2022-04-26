
namespace studentBARLibrary.DataAcess
{
    public interface IUniversityData
    {
        Task CreateUniversity(UniversityModel university);
        Task<List<UniversityModel>> GetAllUniversities();
        Task UpdateUniversity(UniversityModel university);
    }
}