using MapEntitiesService.Core.Configurations;
using MapEntitiesService.Core.Services;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MapEntitiesService.Infrastructure;

public static class ServicesCollectionExtension
{
    public static void AddMapEntitiesServiceInfrastructureServices(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton(settings);
        services.AddScoped<IMapEntityService, MapEntityService>();
    }
}