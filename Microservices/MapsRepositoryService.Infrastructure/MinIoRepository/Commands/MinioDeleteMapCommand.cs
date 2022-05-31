using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;
using MapsRepositoryService.Infrastructure.MinIoConfiguration;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Commands;

public class MinioDeleteMapCommand : IDeleteMapCommand
{
    private readonly MinioClient _minioClient;
    private readonly Configuration _configuration;

    public MinioDeleteMapCommand(IMinioClientBuilder minioClientBuilder, Configuration configuration)
    {
        _minioClient = minioClientBuilder.Build();
        _configuration = configuration;
    }
    public async Task<ResultModel> DeleteMapAsync(string mapName)
    {
        try
        {
            var args = new RemoveObjectArgs()
            .WithBucket(_configuration.MapsBucket)
            .WithObject(mapName);

            await _minioClient.RemoveObjectAsync(args);

            return new ResultModel(Success: true);
        }
        catch (Exception)
        {
            return new ResultModel(Success: false, ErrorMessage: $"Delete {mapName} failed");
        }
    }
}
