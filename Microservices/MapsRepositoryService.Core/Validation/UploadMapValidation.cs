using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Interfaces;
using MapsRepositoryService.Core.Validation.Validators;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapsRepositoryService.Core.Validation;

public class UploadMapValidation : IUploadMapValidation
{
    private readonly IFileExtensionValidator _fileExtensionValidator;
    private readonly IMapNameValidator _mapNameValidator;

    //File is not empty

    //File size is not over 1mb
    //    File name(our file name) will not be empty(field required)

    //Indication on each validation(1st one) -> print it
    //File name should be unique(check in MinIo maps names) [Last]
    //File name should be valid(letters and numbers) [REGEX]
    //Valid extensions.jpeg, .png, .jpg, .svg
    //    All validations should be in core / validations

    public UploadMapValidation(
        IFileExtensionValidator fileExtensionValidator,
        IMapNameValidator mapNameValidator)
    {
        _fileExtensionValidator = fileExtensionValidator;
        _mapNameValidator = mapNameValidator;
    }
    public ResultModel Validate(MapModel mapModel)
    {
        ResultModel validationResult = _fileExtensionValidator.IsFileExtensionValid(mapModel.Extension);

        if(validationResult.Success is true)
            validationResult = _mapNameValidator.Validate(mapModel);

        return validationResult;
    }
}