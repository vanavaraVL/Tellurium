namespace FarmManagement.API.Middleware.Extensions;

public static class DependencyInjection
{
    public static void RegisterMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<FarmerExceptionHandlingMiddleware>();
    }
}