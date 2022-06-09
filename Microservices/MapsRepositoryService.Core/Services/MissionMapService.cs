using MapsRepositoryService.Core.Configuration;
using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository;
using MapsRepositoryService.Core.Services.Interfaces;
using MessageBroker.Core;
using Microsoft.Extensions.Logging;

namespace MapsRepositoryService.Core.Services;

public class MissionMapService : IMissionMapService
{
    private readonly IMapsRepository _mapsRepository;
    private readonly ILogger<MissionMapService> _logger;
    private readonly IPublisher _publisher;
    private readonly MessageBrokerSettings _messageBrokerSettings;

    public MissionMapService(
        IMapsRepository mapsRepository,
        ILogger<MissionMapService> logger,
        IPublisher publisher, MessageBrokerSettings messageBrokerSettings)
    {
        _mapsRepository = mapsRepository;
        _logger = logger;
        _publisher = publisher;
        _messageBrokerSettings = messageBrokerSettings;
    }

    public async Task<MapResultModel> GetMissionMapBase64Async() => await _mapsRepository.GetMissionMapBase64Async();

    public async Task<ResultModel> SetMissionMapAsync(string mapName)
    {
        try
        {
            var result = await _mapsRepository.SetMissionMapAsync(mapName);
            _publisher.Publish(_messageBrokerSettings.MissionMapTopic, mapName);

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, message: "Set mission map failed.");
            return new ResultModel(Success: false, ErrorMessage: exception.Message);
        }
    }
}