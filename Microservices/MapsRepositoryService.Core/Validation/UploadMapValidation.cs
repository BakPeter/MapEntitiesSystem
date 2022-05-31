using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Interfaces;

namespace MapsRepositoryService.Core.Validation;

public class UploadMapValidation : IUploadMapValidation
{
    //File is not empty

    //File size is not over 1mb
    //    File name(our file name) will not be empty(field required)

    //Indication on each validation(1st one) -> print it
    //File name should be unique(check in MinIo maps names) [Last]
    //File name should be valid(letters and numbers) [REGEX]
    //Valid extensions.jpeg, .png, .jpg, .svg
    //    All validations should be in core / validations

    public ResultModel Validate(MapModel mapModel)
    {
        throw new NotImplementedException();
    }
}