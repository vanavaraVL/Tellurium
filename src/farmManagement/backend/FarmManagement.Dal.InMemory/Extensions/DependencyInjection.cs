using FarmManagement.Dal.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;
using FarmManagement.Dal.Repositories;

namespace FarmManagement.Dal.InMemory.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInMemoryStorage(this IServiceCollection services)
    {
        services.AddSingleton<IAnimalRepository, AnimalRepository>();

        return services;
    }
}