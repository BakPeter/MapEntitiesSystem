using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Validation.Validators.Interfaces;

namespace MapEntitiesService.Core.Validation.Validators;

public class MapEntityNameValidator : IMapEntityNameValidator
{
    public ResultModel Validate(MapEntityModel mapEntityModel) => !string.IsNullOrEmpty(mapEntityModel.Title) ? new ResultModel(true) : new ResultModel(false, "Title is empty");
}