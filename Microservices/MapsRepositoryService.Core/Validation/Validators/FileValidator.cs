using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;

namespace MapsRepositoryService.Core.Validation.Validators;

public class FileValidator : IFileValidator
{
    public ResultModel ValidateFile(Stream? stream) =>
            stream == null ? new ResultModel(Success: false, ErrorMessage:
            "File data is null") : new ResultModel(Success: true);

    public ResultModel ValidateSize(Stream? stream)
    {
        var validateFile = ValidateFile(stream);
        if (validateFile.Success is false) return validateFile;

        const int megaByte = 1048576;
        return stream is { Length: > megaByte } ?
        new ResultModel(Success: false, ErrorMessage: "File over 1mb") :
        new ResultModel(Success: true);
    }
}