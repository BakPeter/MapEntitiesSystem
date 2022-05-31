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

    public async Task<ResultModel> AddMapAsync(MapModel mapModel)
    {
        try
        {
            var args = new PutObjectArgs()
                    .WithBucket(_configuration.MapsBucket)
                    .WithObject(mapModel.Name)
                    .WithStreamData(mapModel.Data)
                    .WithObjectSize(mapModel.Data is null ? 0 : mapModel.Data.Length);

            await _minioClient.PutObjectAsync(args);

            return new ResultModel(Success: true);
        }
        catch (Exception)
        {
            return new ResultModel(Success: false, ErrorMessage: $"Add {mapModel.Name} failed");
        }
    }

    public Task<ResultModel> AddMapAsync(MapModel mapModel)
    {
        throw new NotImplementedException();
    }
}
