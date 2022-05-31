using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Commands;

internal class MinIoAddMapCommand : IAddMapCommand
{
    private readonly MinioClient _minIoClient;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoAddMapCommand(MinIoClientBuilder minIoClientBuilder, MinIoConfiguration minIoConfiguration)
    {
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<ResultModel> AddMapAsync(MapModel mapModel)
    {
        try
        {
            var args = new PutObjectArgs()
                    .WithBucket(_minIoConfiguration.MapsBucket)
                    .WithObject($"{mapModel.Name}{mapModel.Extension}")
                    .WithStreamData(mapModel.Data)
                    .WithObjectSize(mapModel.Data?.Length ?? 0);

            await _minIoClient.PutObjectAsync(args);

            return new ResultModel(Success: true);
        }
        catch (Exception)
        {
            return new ResultModel(Success: false, ErrorMessage: $"Add {mapModel.Name} failed");
        }
    }
}
