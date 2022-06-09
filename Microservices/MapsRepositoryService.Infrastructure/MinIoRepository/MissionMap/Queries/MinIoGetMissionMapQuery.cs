using System.Reactive.Linq;
using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.MissionMap.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Queries;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.MissionMap.Queries;

internal class MinIoGetMissionMapQuery : IGetMissionMapQuery
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoGetMapBase64Query> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoGetMissionMapQuery(ILogger<MinIoGetMapBase64Query> logger,
                              MinIoClientBuilder minIoClientBuilder,
                              MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<MapResultModel> GetMissionMapBase64Async()
    {
        try
        {
            var listObjectArgs = new ListObjectsArgs()
                             .WithBucket(_minIoConfiguration.MissionMapBucket)
                             .WithRecursive(true);
            var item = await _minIoClient.ListObjectsAsync(listObjectArgs).FirstOrDefaultAsync();

            if (item == null)
                return new MapResultModel { ErrorMessage = "No mission map found", Success = false };
            
            var memoryStream = new MemoryStream();
            var args = new GetObjectArgs()
                       .WithBucket(_minIoConfiguration.MapsBucket)
                       .WithObject(item.Key)
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
                ErrorMessage = "Get mission map failed"
            };
            return result;
        }
    }
}