using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Validation.Validators.Interfaces;

public interface IFileValidator
{
    ResultModel Validate(MapModel mapModel);
}