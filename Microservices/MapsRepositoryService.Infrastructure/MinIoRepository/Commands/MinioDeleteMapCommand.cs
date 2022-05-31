using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Commands;

internal class MinIoDeleteMapCommand : IDeleteMapCommand
{
    private readonly MinioClient _minIoClient;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoDeleteMapCommand(MinIoClientBuilder minIoClientBuilder, MinIoConfiguration minIoConfiguration)
    {
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }
    public async Task<ResultModel> DeleteMapAsync(string mapName)
    {
        try
        {
            var args = new RemoveObjectArgs()
            .WithBucket(_minIoConfiguration.MapsBucket)
            .WithObject(mapName);

            await _minIoClient.RemoveObjectAsync(args);

            return new ResultModel(Success: true);
        }
        catch (Exception)
        {
            return new ResultModel(Success: false, ErrorMessage: $"Delete {mapName} failed");
        }
    }
}
