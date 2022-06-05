using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.Exceptions;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Queries;

internal class MinIoIsMapNameUniqQuery : IIsMapNameUniqQuery
{
    private readonly ILogger<MinIoIsMapNameUniqQuery> _logger;

    private readonly MinioClient _minIoClient;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoIsMapNameUniqQuery(
        ILogger<MinIoIsMapNameUniqQuery> logger,
        MinIoClientBuilder minIoClientBuilder,
        MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<IsMapNameUniqResultModel> IsMapNameUniq(MapNameModel mapNameModel)
    {
        try
        {
            var args = new StatObjectArgs()
            .WithBucket(_minIoConfiguration.MapsBucket)
            .WithObject(mapNameModel.mapName + mapNameModel.mapExtension);

            var result = await _minIoClient.StatObjectAsync(args);

            return new IsMapNameUniqResultModel(Success: true, NameUniq: false);

        }
        catch (ObjectNotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            return new IsMapNameUniqResultModel(Success: true, NameUniq: true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new IsMapNameUniqResultModel(Success: false, ErrorMessage: ex.Message);
        }
    }
}
