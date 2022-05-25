using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;
using MapsRepositoryService.Infrastructure.MinIoConfiguration;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Queries;

public class MinIoGetMapDataQuery : IGetMapDataQuery
{
    private readonly MinioClient _minioClient;
    private readonly Configuration _configuration;

    public MinIoGetMapDataQuery(IMinioClientBuilder minioClientBuilder, Configuration configuration)
    {
        _minioClient = minioClientBuilder.Build();
        _configuration = configuration;
    }
    public async Task<MapResultModel> GetMapDataAsync(string mapName)
    {
        try
        {
            var buffer = Array.Empty<byte>();

            var args = new GetObjectArgs()
                        .WithBucket(_configuration.MapsBucket)
                        .WithObject(mapName)
                        .WithCallbackStream((stream) =>
                        {
                            using MemoryStream ms = new();
                            stream.CopyTo(ms);
                            buffer = ms.ToArray();
                        });
            var stat = await _minioClient.GetObjectAsync(args);

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
