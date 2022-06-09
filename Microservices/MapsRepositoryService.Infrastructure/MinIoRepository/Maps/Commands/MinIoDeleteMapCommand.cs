using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Maps.Commands;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Commands;

internal class MinIoDeleteMapCommand : IDeleteMapCommand
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoDeleteMapCommand> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoDeleteMapCommand(
        ILogger<MinIoDeleteMapCommand> logger, 
        MinIoClientBuilder minIoClientBuilder,
        MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
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
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return new ResultModel(Success: false, ErrorMessage: $"Delete {mapName} failed");
        }
    }
}
