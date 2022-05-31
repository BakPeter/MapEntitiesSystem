using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;
using MapsRepositoryService.Infrastructure.MinIoConfiguration;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Commands;

public class MinioAddMapCommand : IAddMapCommand
{
    private readonly MinioClient _minioClient;
    private readonly Configuration _configuration;

    public MinioAddMapCommand(IMinioClientBuilder minioClientBuilder, Configuration configuration)
    {
        _minioClient = minioClientBuilder.Build();
        _configuration = configuration;
    }

    public async Task<ResultModel> AddMapAsync(string mapName, byte[] mapData)
    {
        try
        {
            using (var filestream = new MemoryStream(mapData))
            {
                var args = new PutObjectArgs()
                    .WithBucket(_configuration.MapsBucket)
                    .WithObject(mapName)
                    .WithStreamData(filestream)
                    .WithObjectSize(filestream.Length);

                await _minioClient.PutObjectAsync(args);
            }

            return new ResultModel(Success: true);
        }
        catch (Exception)
        {
            return new ResultModel(Success: false, ErrorMessage: $"Add {mapName} failed");
        }
    }

    public Task<ResultModel> AddMapAsync(MapModel mapModel)
    {
        throw new NotImplementedException();
    }
}
