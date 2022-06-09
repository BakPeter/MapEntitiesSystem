using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.MissionMap.Queries;

public interface IGetMissionMapQuery
{
    Task<MapResultModel> GetMissionMapBase64Async();
}