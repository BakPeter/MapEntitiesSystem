using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Maps.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Queries;

internal class MinIoGetMapStreamQuery : IGetMapStreamQuery
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoGetMapStreamQuery> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoGetMapStreamQuery(
        ILogger<MinIoGetMapStreamQuery> logger,
        MinIoClientBuilder minIoClientBuilder,
        MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<MapStreamResultModel> GetMapStream(string mapName)
    {
        try
        {
            var memoryStream = new MemoryStream();
            var args = new GetObjectArgs()
                .WithBucket(_minIoConfiguration.MapsBucket)
                .WithObject(mapName)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                });
            await _minIoClient.GetObjectAsync(args);

            var result = new MapStreamResultModel(Success: true, ErrorMessage: "", Stream: memoryStream);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            var result = new MapStreamResultModel(Success: false, ErrorMessage: $"Map {mapName} does not exist", Stream: null);
            return result;
        }
    }
}