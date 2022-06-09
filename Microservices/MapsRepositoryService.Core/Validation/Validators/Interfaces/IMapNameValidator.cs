using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Validation.Validators.Interfaces;

public interface IMapNameValidator
{
    ResultModel MapNameNotEmpty(string mapName);
    ResultModel MapNameIsValid(string mapName);
    ResultModel IsMapNameUnique(MapNameModel mapNameModel);
}