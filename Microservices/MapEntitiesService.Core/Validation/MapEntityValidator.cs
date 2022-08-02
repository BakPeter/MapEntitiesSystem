using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Validation.Interfaces;
using MapEntitiesService.Core.Validation.Validators.Interfaces;
using Microsoft.Extensions.Logging;

namespace MapEntitiesService.Core.Validation;

public class MapEntityValidator : IMapEntityValidator
{
    private readonly ILogger<MapEntityValidator> _logger;
    private readonly IMapEntityNameValidator _entityNameValidator;

    public MapEntityValidator(ILogger<MapEntityValidator> logger, IMapEntityNameValidator entityNameValidator)
    {
        _logger = logger;
        _entityNameValidator = entityNameValidator;
    }

    public ResultModel Validate(MapEntityModel mapEntityModel)
    {
       var result=  _entityNameValidator.Validate(mapEntityModel);
       
       _logger.LogInformation(
           "Services project: service={service}, method={method}, dto={dto}, validationResult={validationResult}",
           "MapEntityValidationService",
           "Validate", mapEntityModel, result.Success);

        return result;
    }
}