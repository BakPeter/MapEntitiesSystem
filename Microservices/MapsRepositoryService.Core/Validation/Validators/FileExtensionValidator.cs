using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapsRepositoryService.Core.Validation.Validators;

public class FileExtensionValidator : IFileExtensionValidator
{
    public ResultModel Validate(MapModel mapModel)
    {
        var extensions = new[] { ".jpg", ".jpeg", ".png", ".svg" };
        var isExtensionExists = extensions.Contains(mapModel.Extension.ToLower());
        
        return isExtensionExists ? new ResultModel(Success: true) : 
                                   new ResultModel(Success: false, ErrorMessage: "File extension not allowed");
    }
}