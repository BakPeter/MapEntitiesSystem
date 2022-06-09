using System.Reactive.Linq;
using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.MissionMap.Commands;
using MapsRepositoryService.Infrastructure.MinIoDb;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.MissionMap.Commands;

internal class MinIoDeleteMissionMapCommand : IDeleteMissionMapCommand
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoDeleteMissionMapCommand> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;

    public MinIoDeleteMissionMapCommand(
        ILogger<MinIoDeleteMissionMapCommand> logger,
        MinIoClientBuilder minIoClientBuilder,
        MinIoConfiguration minIoConfiguration)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
    }

    public async Task<ResultModel> DeleteMissionMap()
    {
        try
        {
            var listObjectArgs = new ListObjectsArgs()
                .WithBucket(_minIoConfiguration.MissionMapBucket)
                .WithRecursive(true);
            var item = await _minIoClient.ListObjectsAsync(listObjectArgs).FirstOrDefaultAsync();

            if (item == null)
                return new ResultModel(true);

            var removeArgs = new RemoveObjectArgs().WithBucket(_minIoConfiguration.MissionMapBucket)
                .WithObject(item.Key);
            await _minIoClient.RemoveObjectAsync(removeArgs);

            return new ResultModel(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ResultModel(Success: false, ErrorMessage: "Delete mission map failed");
        }
    }
}