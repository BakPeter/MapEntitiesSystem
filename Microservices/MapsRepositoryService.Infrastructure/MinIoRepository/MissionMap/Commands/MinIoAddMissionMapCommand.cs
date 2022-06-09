using System.Reactive.Linq;
using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Maps.Queries;
using MapsRepositoryService.Core.Repository.MissionMap.Commands;
using MapsRepositoryService.Infrastructure.MinIoDb;
using MapsRepositoryService.Infrastructure.MinIoRepository.Maps.Commands;
using Microsoft.Extensions.Logging;
using Minio;

namespace MapsRepositoryService.Infrastructure.MinIoRepository.MissionMap.Commands;

internal class MinIoAddMissionMapCommand : IAddMissionMapCommand
{
    private readonly MinioClient _minIoClient;
    private readonly ILogger<MinIoAddMapCommand> _logger;
    private readonly MinIoConfiguration _minIoConfiguration;
    private readonly IGetMapStreamQuery _getMapStreamQuery;
    private readonly IDeleteMissionMapCommand _deleteMissionMapCommand;

    public MinIoAddMissionMapCommand(ILogger<MinIoAddMapCommand> logger,
                                     MinIoClientBuilder minIoClientBuilder,
                                     MinIoConfiguration minIoConfiguration, 
                                     IGetMapStreamQuery getMapStreamQuery,
                                     IDeleteMissionMapCommand deleteMissionMapCommand)
    {
        _logger = logger;
        _minIoClient = minIoClientBuilder.Build();
        _minIoConfiguration = minIoConfiguration;
        _getMapStreamQuery = getMapStreamQuery;
        _deleteMissionMapCommand = deleteMissionMapCommand;
    }

    public async Task<ResultModel> AddMissionMapAsync(string mapName)
    {
        try
        {
            var getMapStream = await _getMapStreamQuery.GetMapStream(mapName);
            if (getMapStream.Success is false || getMapStream.Stream == null)
                return new ResultModel(false, getMapStream.ErrorMessage);

            var deleteMissionMap = await _deleteMissionMapCommand.DeleteMissionMap();
            if (deleteMissionMap.Success is false)
                return deleteMissionMap;

            var addMissionMap = await AddMissionMap(mapName, getMapStream.Stream);
            return addMissionMap.Success is false ?
                addMissionMap :
                new ResultModel(Success: true);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new ResultModel(Success: false, ErrorMessage: $"Set mission map {mapName} failed");
        }
    }

    private async Task<ResultModel> AddMissionMap(string name, Stream stream)
    {
        try
        {
            var args = new PutObjectArgs()
                  .WithBucket(_minIoConfiguration.MissionMapBucket)
                  .WithObject(name)
                  .WithStreamData(stream)
                  .WithObjectSize(stream.Length);

            await _minIoClient.PutObjectAsync(args);

            return new ResultModel(Success: true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ResultModel(Success: false, ErrorMessage: $"Add mission map {name} failed");
        }
    }
}