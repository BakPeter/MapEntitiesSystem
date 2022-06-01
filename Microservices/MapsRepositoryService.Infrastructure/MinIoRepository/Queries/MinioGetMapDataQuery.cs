using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Queries;

internal class MinIoGetMapDataQuery : IGetMapDataQuery
{
    private readonly MinioClient _minIoClient;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoGetMapDataQuery(MinIoClientBuilder minIoClientBuilder, MinIoConfiguration minIoConfiguration)
    {
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }
    public async Task<MapResultModel> GetMapByNameAsync(string mapName)
    {
        try
        {
            var memoryStream = new MemoryStream();

            var args = new GetObjectArgs()
                        .WithBucket(_minIoConfiguration.MapsBucket)
                        .WithObject(mapName)
                        .WithCallbackStream(stream => { stream.CopyTo(memoryStream); });
            var stat = await _minIoClient.GetObjectAsync(args);

            var result = new MapResultModel
            {
                Success = true,
                MapBase64 = Convert.ToBase64String(memoryStream.ToArray()),
                ErrorMessage = ""
            };

            return result;
        }
        catch (Exception)
        {
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
