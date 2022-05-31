using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Interfaces;
using MapsRepositoryService.Core.Validation.Validators;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapsRepositoryService.Core.Validation;

public class UploadMapValidation : IUploadMapValidation
{
    private readonly IFileExtensionValidator _fileExtensionValidator;
    private readonly IMapNameValidator _mapNameValidator;
    private readonly IFileValidator _fileValidator;

    public UploadMapValidation(
        IFileExtensionValidator fileExtensionValidator,
        IMapNameValidator mapNameValidator,
        IFileValidator fileValidator)
    {
        _fileExtensionValidator = fileExtensionValidator;
        _mapNameValidator = mapNameValidator;
        _fileValidator = fileValidator;
    }
    public ResultModel Validate(MapModel mapModel)
    {
        ResultModel validationResult = _fileExtensionValidator.IsFileExtensionValid(mapModel.Extension);

        if (validationResult.Success is false)
            return _mapNameValidator.Validate(mapModel);

        if (validationResult.Success is false)
            return _fileValidator.Validate(mapModel);

        return validationResult;
    }
}