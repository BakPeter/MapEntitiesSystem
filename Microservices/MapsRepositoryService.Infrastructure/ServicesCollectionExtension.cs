using MapsRepositoryService.Core.Configuration;
using MapsRepositoryService.Core.Services;
using MapsRepositoryService.Core.Services.Interfaces;
using MapsRepositoryService.Core.Services.Interfaces.Repository;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoRepository.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MapsRepositoryService.Infrastructure;

public static class ServicesCollectionExtension
{
    public static void AddMapEntitiesServiceInfrastructureServices(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton(settings);
        services.AddScoped<IMapsService, MapsService>();
        services.AddScoped<IMapsRepository, MapsRepository>();
        services.AddScoped<IGetMapDataQuery, MinIoGetMapDataQuery>();
    }
}