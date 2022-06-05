using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Validation.Validators.Interfaces;

public interface IMapNameValidator
{
    ResultModel MapNameNotEmpty(string mapName);
    ResultModel AreCharachtersChoisesForMapNameValid(string mapName);
    ResultModel IsMapNameUnique(string mapName);
}