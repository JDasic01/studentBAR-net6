namespace studentBARUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMemoryCache();

        builder.Services.AddSingleton<IDbConnection, DbConnection>();
        builder.Services.AddSingleton<IFacultyData, MongoFacultyData>();
        builder.Services.AddSingleton<IUniversityData, MongoUniversityData>();
        builder.Services.AddSingleton<IPostData, MongoPostData>();
        builder.Services.AddSingleton<IUserData, MongoUserData>();
        builder.Services.AddSingleton<ICourseData, MongoCourseData>();
    }
}
