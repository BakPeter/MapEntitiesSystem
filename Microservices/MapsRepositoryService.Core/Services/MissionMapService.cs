using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository;
using MapsRepositoryService.Core.Services.Interfaces;

namespace MapsRepositoryService.Core.Services;

public class MissionMapService : IMissionMapService
{
    private readonly IMapsRepository _mapsRepository;

    public MissionMapService(IMapsRepository mapsRepository)
    {
        _mapsRepository = mapsRepository;
    }

    public async Task<MapResultModel> GetMissionMapBase64Async() => await _mapsRepository.GetMissionMapBase64Async();

    public async Task<ResultModel> SetMissionMapAsync(string mapName) => await _mapsRepository.SetMissionMapAsync(mapName);
}