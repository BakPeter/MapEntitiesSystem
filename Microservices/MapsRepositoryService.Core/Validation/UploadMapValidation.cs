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
        var result = ValidateMapName(mapModel.Name);

        if(result.Success is false) return result;

        return ValidateFile(mapModel.Data);
        //if (validationResult.Success is false)
        //    return _mapNameValidator.MapNameNotEmpty(mapModel);

        //if (validationResult.Success is false)
        //    return _fileValidator.ValidateFile(mapModel);

        //var validationResult = _fileExtensionValidator.ValidateFile(mapModel);
        //return new ResultModel(Success: true);
    }

    private ResultModel ValidateMapName(string mapName)
    {
        var result = _mapNameValidator.MapNameNotEmpty(mapName);
        if (result.Success is false) return result;

        result = _mapNameValidator.IsMapNameValid(mapName);
        if (result.Success is false) return result;

        result = _mapNameValidator.IsMapNameUnique(mapName);
        if (result.Success is false) return result;

        return new ResultModel(Success: true);
    }

    private ResultModel ValidateFile(Stream? stream)
    {
        var result = _fileValidator.ValidateFile(stream);
        return result.Success is false ? result : _fileValidator.ValidateSize(stream);
    }
}