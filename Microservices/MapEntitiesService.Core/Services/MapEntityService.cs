using MapEntitiesService.Core.Configurations;
using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using MessageBroker.Core;
using Microsoft.Extensions.Logging;

namespace MapEntitiesService.Core.Services;

public class MapEntityService : IMapEntityService
{
    private readonly ILogger<MapEntityService> _logger;
    private readonly IPublisher _publisher;
    private readonly Settings _settings;

    public MapEntityService(
        ILogger<MapEntityService> logger,
        IPublisher publisher,
        Settings settings)
    {
        _logger = logger;
        _publisher = publisher;
        _settings = settings;
    }

    public async Task<ResultModel> HandleMapEntityAsync(MapEntityModel mapEntityModel)
    {
        try
        {
            // Todo - publish to message broker (what,where)
            _publisher.Publish(_settings.Topic, mapEntityModel.ToString());
            await Task.Delay(100);
            return new ResultModel(Success: true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, message: "HandleMapEntityAsync method failed.");
            return new ResultModel(Success: false, ErrorMessage: e.Message);
        }
    }
}