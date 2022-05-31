using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapsRepositoryService.Core.Validation.Validators;

public class FileValidator : IFileValidator
{
    public ResultModel Validate(MapModel mapModel)
    {
        if (mapModel.Data == null)
        {
            return new ResultModel(Success: false, ErrorMessage: "File data is null");
        }

        const int megaByte = 1048576;
        if (mapModel.Data.Length > megaByte)
        {
            return new ResultModel(Success: false, ErrorMessage: "File over 1mb");
        }

        return new ResultModel(Success: true);
    }
}