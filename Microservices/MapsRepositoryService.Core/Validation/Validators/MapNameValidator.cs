using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;
using System.Text.RegularExpressions;
using MapsRepositoryService.Core.Services.Interfaces.Repository;

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

    public ResultModel IsMapNameValid(string mapName)
    {
        var regex = new Regex(@"[a-zA-Z0-9_.-]*$");
        var match = regex.Match(mapName);
        return match.Success is false ? 
            new ResultModel(Success: false, ErrorMessage: "ILegal characters for file name") : 
            new ResultModel(Success: true);
    }

    public ResultModel IsMapNameUnique(string mapName)
    {
        var isExits = _mapsRepository.IsMapNameExits(mapName);
        return isExits == false ? 
            new ResultModel(Success: true) : 
            new ResultModel(Success: false, ErrorMessage: "Todo Peter");
    }
}