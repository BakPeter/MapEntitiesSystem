using MapEntitiesService.Core.Configurations;
using MapEntitiesService.Core.Services;
using MapEntitiesService.Core.Services.Interfaces;
using MapEntitiesService.Core.Validation;
using MapEntitiesService.Core.Validation.Interfaces;
using MapEntitiesService.Core.Validation.Validators;
using MapEntitiesService.Core.Validation.Validators.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MapEntitiesService.Infrastructure;

public static class ServicesCollectionExtension
{
    public static void AddMapEntitiesServiceInfrastructureServices(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton(settings);
        services.AddScoped<IMapEntityService, MapEntityService>();
        services.AddSingleton<IMapEntityNameValidator, MapEntityNameValidator>();
        services.AddSingleton<IMapEntityValidator, MapEntityValidator>();
    }
}