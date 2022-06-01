using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository;

public interface IMapsRepository
{
    Task<MapResultModel> GetMapDataAsync(string mapName);
    Task<MapNamesResultModel> GetMapsNamesAsync();
    Task<ResultModel> AddMapAsync(MapModel mapModel);
    Task<ResultModel> DeleteMapAsync(string mapName);
    bool IsMapNameExits(string mapName);
}
