using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.Services.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IFarmerService, FarmerService>();

        return services;
    }
}