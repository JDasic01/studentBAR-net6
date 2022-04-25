
namespace studentBARLibrary.DataAcess
{
    public interface IUniversityData
    {
        Task CreateSuggestion(UniversityModel university);
        Task<List<UniversityModel>> GetAllSuggestions();
        Task UpdateSuggestion(UniversityModel university);
    }
}