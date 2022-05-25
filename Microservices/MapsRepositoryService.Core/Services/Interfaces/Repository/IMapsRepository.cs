using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository;

public interface IMapsRepository
{
    Task<MapResultModel> GetMapDataAsync(string mapName);
    Task<MapNamesResultModel> GetMapsNamesAsync();
    Task<ResultModel> AddMapAsync(string mapName, byte[] mapData);
    Task<ResultModel> DeleteMapAsync(string mapName);

}
