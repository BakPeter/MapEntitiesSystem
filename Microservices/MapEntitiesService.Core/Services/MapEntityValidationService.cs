using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapEntitiesService.Core.Services
{
    public class MapEntityValidationService : IMapEntityValidationService
    {
        private readonly ILogger<MapEntityValidationService> _logger;

        public MapEntityValidationService(ILogger<MapEntityValidationService> logger)
        {
            _logger = logger;
        }

        public ResultModel Validate(MapEntityModel entityModel)
        {
            //todo - implement EntityModel validation after defined by David

            var validationResult = true;

            _logger.LogInformation(
                "Services project: service={service}, method={method}, dto={dto}, validationResult={validationResult}",
                "MapEntityValidationService",
                "Validate",
                entityModel,
                validationResult);
            return new ResultModel(validationResult);
            //new(false, "Validation error message");
        }
    }
}
