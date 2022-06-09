using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.MissionMap.Commands;

public interface IAddMissionMapCommand
{
    Task<ResultModel> AddMissionMapAsync(string mapName);
}