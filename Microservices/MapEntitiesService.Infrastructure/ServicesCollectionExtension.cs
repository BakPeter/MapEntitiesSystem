using MapEntitiesService.Core.Services;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MapEntitiesService.Infrastructure;

public static class ServicesCollectionExtension
{
    public static void AddMapEntitiesServiceInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IMapEntityService, MapEntityService>();
    }
}