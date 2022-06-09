using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces;

public interface IMissionMapService
{
    Task<MapResultModel> GetMissionMapBase64Async();
    Task<ResultModel> SetMissionMapAsync(string mapName);
}