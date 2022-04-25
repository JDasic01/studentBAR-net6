namespace studentBARUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMemoryCache();

        //builder.Services.AddSingleton<IDbConnection, DbConnection>();
        //builder.Services.AddSingleton<IfacultyData, MongofacultyData>();
        //builder.Services.AddSingleton<IStatusData, MongoStatusData>();
        //builder.Services.AddSingleton<IpostData, MongopostData>();
        //builder.Services.AddSingleton<IUserData, MongoUserData>();
    }
}
