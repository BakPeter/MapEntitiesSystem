using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;
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
    public async Task<MapResultModel> GetMapDataAsync(string mapName)
    {
        try
        {
            var buffer = new MemoryStream();

            var args = new GetObjectArgs()
                        .WithBucket(_minIoConfiguration.MapsBucket)
                        .WithObject(mapName)
                        .WithCallbackStream(stream => { stream.CopyTo(buffer); });
            var stat = await _minIoClient.GetObjectAsync(args);

            var result = new MapResultModel
            {
                Success = true,
                MapModel = new MapModel
                {
                    Name = stat.ObjectName,
                    Data = buffer
                },
                ErrorMessage = ""
            };

            return result;
        }
        catch (Exception)
        {
            var result = new MapResultModel
            {
                Success = false,
                MapModel = new MapModel(),
                ErrorMessage = $"Get Map {mapName} failed"
            };
            return result;
        }
    }
}
