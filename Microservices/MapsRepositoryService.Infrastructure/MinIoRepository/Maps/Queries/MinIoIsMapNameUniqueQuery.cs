using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Maps.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.Exceptions;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Queries;

internal class MinIoIsMapNameUniqueQuery : IIsMapNameUniqueQuery
{
    private readonly ILogger<MinIoIsMapNameUniqueQuery> _logger;
    private readonly MinioClient _minIoClient;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoIsMapNameUniqueQuery(
        ILogger<MinIoIsMapNameUniqueQuery> logger,
        MinIoClientBuilder minIoClientBuilder,
        MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public IsMapNameUniqResultModel IsMapNameUnique(MapNameModel mapNameModel)
    {
        try
        {
            var args = new StatObjectArgs()
            .WithBucket(_minIoConfiguration.MapsBucket)
            .WithObject(mapNameModel.mapName + mapNameModel.mapExtension);

            _minIoClient.StatObjectAsync(args).GetAwaiter().GetResult();
            return new IsMapNameUniqResultModel(Success: true, NameUnique: false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return ex switch
            {
                ObjectNotFoundException => new IsMapNameUniqResultModel(Success: true, NameUnique: true),
                _ => new IsMapNameUniqResultModel(Success: false, ErrorMessage: ex.Message)
            };
        }
    }
}
