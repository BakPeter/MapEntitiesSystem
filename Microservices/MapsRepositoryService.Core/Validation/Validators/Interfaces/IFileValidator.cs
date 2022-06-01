using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Validation.Validators.Interfaces;

public interface IFileValidator
{
    ResultModel ValidateFile(Stream? stream);
    ResultModel ValidateSize(Stream? stream);
}