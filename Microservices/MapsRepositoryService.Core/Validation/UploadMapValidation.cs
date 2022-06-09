using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Interfaces;
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
        var validationResult = _fileExtensionValidator.Validate(mapModel);
        if (validationResult.Success is false) return validationResult;

        validationResult = ValidateMapName(new MapNameModel(mapModel.Name, mapModel.Extension));
        return validationResult.Success is false ? validationResult : ValidateFile(mapModel.Data);
    }

    private ResultModel ValidateMapName(MapNameModel mapNameModel)
    {
        var result = _mapNameValidator.MapNameNotEmpty(mapNameModel.mapName);
        if (result.Success is false) return result;

        result = _mapNameValidator.MapNameIsValid(mapNameModel.mapName);
        if (result.Success is false) return result;

        result = _mapNameValidator.IsMapNameUnique(mapNameModel);
        return result.Success is false ? result : new ResultModel(Success: true);
    }

    private ResultModel ValidateFile(Stream? stream)
    {
        var result = _fileValidator.ValidateFile(stream);
        return result.Success is false ? result : _fileValidator.ValidateFileSize(stream);
    }
}