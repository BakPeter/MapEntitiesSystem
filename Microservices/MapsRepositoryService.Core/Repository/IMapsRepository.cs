using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository;

public interface IMapsRepository
{
    Task<MapResultModel> GetMapDataAsync(string mapName);
    Task<MapNamesResultModel> GetMapsNamesAsync();
    Task<ResultModel> AddMapAsync(MapModel mapModel);
    Task<ResultModel> DeleteMapAsync(string mapName);
    IsMapNameUniqResultModel IsMapNameUnique(MapNameModel mapNameModel);
    Task<MapResultModel> GetMissionMapBase64Async();
    Task<ResultModel> SetMissionMapAsync(string mapName);
    Task<MapStreamResultModel> GetMapStream(string mapName);
}
