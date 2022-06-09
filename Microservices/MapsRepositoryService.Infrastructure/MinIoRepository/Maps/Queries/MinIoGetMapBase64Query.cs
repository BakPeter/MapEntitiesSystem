using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Maps.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Queries;

internal class MinIoGetMapBase64Query : IGetMapBase64Query
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoGetMapBase64Query> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoGetMapBase64Query(
        ILogger<MinIoGetMapBase64Query> logger,
        MinIoClientBuilder minIoClientBuilder,
        MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<MapResultModel> GetMapBase64Async(string mapName)
    {
        try
        {
            var memoryStream = new MemoryStream();

            var args = new GetObjectArgs()
                        .WithBucket(_minIoConfiguration.MapsBucket)
                        .WithObject(mapName)
                        .WithCallbackStream(stream => { stream.CopyTo(memoryStream); });
            await _minIoClient.GetObjectAsync(args);

            var result = new MapResultModel
            {
                Success = true,
                MapBase64 = Convert.ToBase64String(memoryStream.ToArray()),
                ErrorMessage = ""
            };

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            var result = new MapResultModel
            {
                Success = false,
                MapBase64 = string.Empty,
                ErrorMessage = $"Get Map {mapName} failed"
            };
            return result;
        }
    }
}
