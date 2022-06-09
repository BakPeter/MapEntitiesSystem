using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.MissionMap.Commands;

public interface IDeleteMissionMapCommand
{
    Task<ResultModel> DeleteMissionMap();
}