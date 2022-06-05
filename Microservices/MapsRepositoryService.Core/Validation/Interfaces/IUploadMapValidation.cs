using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Validation.Interfaces;

public interface IUploadMapValidation
{
    ResultModel Validate(MapModel mapModel);
}
