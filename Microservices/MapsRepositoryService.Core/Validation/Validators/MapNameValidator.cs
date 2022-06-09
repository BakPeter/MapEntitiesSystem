using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;
using System.Text.RegularExpressions;
using MapsRepositoryService.Core.Repository;

namespace MapsRepositoryService.Core.Validation.Validators;

public class MapNameValidator : IMapNameValidator
{
    private readonly IMapsRepository _mapsRepository;

    public MapNameValidator(IMapsRepository mapsRepository)
    {
        _mapsRepository = mapsRepository;
    }

    public ResultModel MapNameNotEmpty(string mapName)
    {
        return mapName.Equals(string.Empty) ?
            new ResultModel(Success: false, ErrorMessage: "Map name is empty") :
            new ResultModel(Success: true);
    }

    public ResultModel MapNameIsValid(string mapName)
    {
        var regex = new Regex(@"[a-zA-Z0-9_.-]*$");
        var match = regex.Match(mapName);
            
        return match.Success is false ?
            new ResultModel(Success: false, ErrorMessage: "Illegal characters for file name") :
            new ResultModel(Success: true);
    }

    public ResultModel IsMapNameUnique(MapNameModel mapNameModel)
    {
        var result = _mapsRepository.IsMapNameUnique(mapNameModel); 

        if(result.NameUnique is false)
            return new ResultModel(false, "Map name already exists");

        return result.Success is false ? 
            new ResultModel(Success: false, ErrorMessage: result.ErrorMessage) : 
            new ResultModel(Success: true);
    }
}