using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapsRepositoryService.Core.Validation.Validators;

public class FileExtensionValidator : IFileExtensionValidator
{
    public ResultModel IsFileExtensionValid(string extension)
    {
        var extensions = new[] { ".jpg", ".jpeg", ".png", ".svg" };
        bool isExtensionExists = extensions.Contains(extension);
        if (isExtensionExists)
        {
            return new ResultModel(Success: true);
        }
        else
        {
            return new ResultModel(Success: false, ErrorMessage: "File extension not allowed");
        }
    }
}