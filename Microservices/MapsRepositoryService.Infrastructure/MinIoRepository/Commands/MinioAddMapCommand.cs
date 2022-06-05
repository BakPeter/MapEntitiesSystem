using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Commands;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.Commands;

internal class MinIoAddMapCommand : IAddMapCommand
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoAddMapCommand> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoAddMapCommand(
        ILogger<MinIoAddMapCommand> logger, 
        MinIoClientBuilder minIoClientBuilder, 
        MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
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
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ResultModel(Success: false, ErrorMessage: $"Add {mapModel.Name} failed");
        }
    }
}
