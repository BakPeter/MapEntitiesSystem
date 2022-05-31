using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Validation.Validators.Interfaces;

public interface IMapNameValidator
{
    ResultModel Validate(MapModel mapModel);
}