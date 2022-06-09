using MapsRepositoryService.Core.Configuration;
using MapsRepositoryService.Core.Repository;
using MapsRepositoryService.Core.Repository.Maps.Commands;
using MapsRepositoryService.Core.Repository.Maps.Queries;
using MapsRepositoryService.Core.Repository.MissionMap.Commands;
using MapsRepositoryService.Core.Repository.MissionMap.Queries;
using MapsRepositoryService.Core.Services;
using MapsRepositoryService.Core.Services.Interfaces;
using MapsRepositoryService.Core.Validation;
using MapsRepositoryService.Core.Validation.Interfaces;
using MapsRepositoryService.Core.Validation.Validators;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;
using MapsRepositoryService.Infrastructure.MinIoDb;
using MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Commands;
using MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Queries;
using MapsRepositoryService.Infrastructure.MinIoRepository.MissionMap.Commands;
using MapsRepositoryService.Infrastructure.MinIoRepository.MissionMap.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MapsRepositoryService.Infrastructure;

public static class ServicesCollectionExtension
{
    public static void AddMapsRepositoryServiceInfrastructureServices(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton(settings);

        services.AddScoped<IMapsService, MapsService>();
        services.AddScoped<IMissionMapService, MissionMapService>();

        services.AddScoped<IMapsRepository, MapsRepository>();
        services.AddScoped<IGetMapBase64Query, MinIoGetMapBase64Query>();
        services.AddScoped<IGetMapsNamesQuery, MinIoGetMapsNamesQuery>();
        services.AddScoped<IDeleteMapCommand, MinIoDeleteMapCommand>();
        services.AddScoped<IAddMapCommand, MinIoAddMapCommand>();
        
        var minIoConfig = new MinIoConfiguration
        {
            Server = settings.Endpoint,
            User = settings.User,
            Password = settings.Password,
            MapsBucket = settings.MapsBucket,
            MissionMapBucket = settings.MissionMapBucket
        };
        services.AddScoped(_ => minIoConfig);
        services.AddScoped<MinIoClientBuilder>();

        services.AddScoped<IUploadMapValidation, UploadMapValidation>();
        services.AddScoped<IFileExtensionValidator, FileExtensionValidator>();
        services.AddScoped<IMapNameValidator, MapNameValidator>();
        services.AddScoped<IFileValidator, FileValidator>();
        services.AddScoped<IIsMapNameUniqueQuery, MinIoIsMapNameUniqueQuery>();
        services.AddScoped<IGetMapStreamQuery, MinIoGetMapStreamQuery>();
        services.AddScoped<IGetMissionMapQuery, MinIoGetMissionMapQuery>();
        services.AddScoped<IAddMissionMapCommand, MinIoAddMissionMapCommand>();
        services.AddScoped<IDeleteMissionMapCommand, MinIoDeleteMissionMapCommand>();
    }
}