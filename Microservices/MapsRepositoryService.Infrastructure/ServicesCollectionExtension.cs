using MapsRepositoryService.Core.Configuration;
using MapsRepositoryService.Core.Services;
using MapsRepositoryService.Core.Services.Interfaces;
using MapsRepositoryService.Core.Services.Interfaces.Repository;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using MapsRepositoryService.Infrastructure.MinIoRepository.Commands;
using MapsRepositoryService.Infrastructure.MinIoRepository.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MapsRepositoryService.Infrastructure;

public static class ServicesCollectionExtension
{
    public static void AddMapsRepositoryServiceInfrastructureServices(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton(settings);
        
        services.AddScoped<IMapsService, MapsService>();
        services.AddScoped<IMapsRepository, MapsRepository>();
        services.AddScoped<IGetMapDataQuery, MinIoGetMapDataQuery>();
        services.AddScoped<IGetMapsNamesQuery, MinIoGetMapsNamesQuery>();
        services.AddScoped<IDeleteMapCommand, MinIoDeleteMapCommand>();
        services.AddScoped<IAddMapCommand, MinIoAddMapCommand>();

        var minIoConfig = new MinIoConfiguration
        {
            Server = settings.Endpoint,
            User = settings.User,
            Password = settings.Password,
            MapsBucket = settings.MapsBucket
        };
        services.AddSingleton(_ => minIoConfig);

        services.AddScoped<MinIoClientBuilder>();
    }
}