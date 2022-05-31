using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces;

public interface IMapsService
{
    MapNamesResultModel GetMapsNames();
    MapResultModel GetMapData(string mapName);
    Task<ResultModel> AddMap(MapModel mapModel);
    ResultModel DeleteMap(string mapName);
}
