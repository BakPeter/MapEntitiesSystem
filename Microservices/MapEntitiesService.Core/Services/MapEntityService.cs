using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapEntitiesService.Core.Services;

public class MapEntityService : IMapEntityService
{
    private readonly ILogger<MapEntityService> _logger;

    public MapEntityService(ILogger<MapEntityService> logger)
    {
        _logger = logger;
    }

    public async Task<ResultModel> HandleMapEntityAsync(MapEntityModel mapEntityModel)
    {
        try
        {
            // Todo - publish to message broker (what,where)
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