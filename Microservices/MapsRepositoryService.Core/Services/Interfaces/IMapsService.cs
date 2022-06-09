using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces;

public interface IMapsService
{
    Task<MapNamesResultModel> GetMapsNamesAsync();
    Task<MapResultModel> GetMapBase64Async(string mapName);
    Task<ResultModel> AddMapAsync(MapModel mapModel);
    Task<ResultModel> DeleteMapAsync(string mapName);
    Task<MapStreamResultModel> GetMapStreamAsync(string mapName);
}
