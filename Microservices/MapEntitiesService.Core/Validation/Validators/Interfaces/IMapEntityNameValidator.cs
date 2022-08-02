using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Validation.Validators.Interfaces;

public interface IMapEntityNameValidator
{
    ResultModel Validate(MapEntityModel mapEntityModel);
}