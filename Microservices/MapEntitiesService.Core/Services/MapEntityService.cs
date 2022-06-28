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
    private readonly IMapEntityValidationService _entityValidator;
    private readonly Settings _settings;

    public MapEntityService(
        ILogger<MapEntityService> logger,
        IPublisher publisher,
        IMapEntityValidationService entityValidator,
        Settings settings)
    {
        _logger = logger;
        _publisher = publisher;
        _entityValidator = entityValidator;
        _settings = settings;
    }

    public async Task<ResultModel> HandleMapEntityAsync(MapEntityModel mapEntityModel)
    {
        try
        {
            _logger.LogInformation(
                "Services project: service={service}, method={method}, dto={dto}",
                "MapEntityService",
                "HandleMapEntityAsync",
                mapEntityModel);
            var validationResult = _entityValidator.Validate(mapEntityModel);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            _publisher.Publish(_settings.Topic, mapEntityModel.ToString());
            await Task.Delay(100);
            return new ResultModel(Success: true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, message: "HandleMapEntityAsync method failed, {errorMessage}", e.Message);
            return new ResultModel(Success: false, ErrorMessage: e.Message);
        }
    }
}