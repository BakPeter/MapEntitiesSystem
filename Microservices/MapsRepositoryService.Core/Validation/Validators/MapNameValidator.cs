using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Validation.Validators.Interfaces;
using System.Text.RegularExpressions;

namespace MapsRepositoryService.Core.Validation.Validators;

public class MapNameValidator : IMapNameValidator
{

    public ResultModel Validate(MapModel mapModel)
    {
        var mapName = mapModel.Name;

        if (mapName == null || mapName.Equals(String.Empty))
            return new ResultModel(Success: false, ErrorMessage: "Map name is empty");

        Regex regex = new Regex(@"[a-zA-Z0-9_.-]*$");
        Match match = regex.Match(mapName);
        if (match.Success is false)
            return new ResultModel(Success: false, ErrorMessage: "ILegal characters for file name");
        
        //TODO - validate map name uniqueness name
        
        return new ResultModel(Success: true);
    }
}