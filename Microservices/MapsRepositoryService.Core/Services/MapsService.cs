using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces;
using MapsRepositoryService.Core.Services.Interfaces.Repository;

namespace MapsRepositoryService.Core.Services;

public class MapsService : IMapsService
{
    private readonly IMapsRepository _mapsRepository;

    public MapsService(IMapsRepository mapsRepository) => _mapsRepository = mapsRepository;

    public MapResultModel GetMapData(string mapName) => _mapsRepository.GetMapDataAsync(mapName).Result;

    public ResultModel AddMap(MapModel mapModel) => _mapsRepository.AddMapAsync(mapModel).Result;

    public ResultModel DeleteMap(string mapName) => _mapsRepository.DeleteMapAsync(mapName).Result;

    public MapNamesResultModel GetMapsNames() => _mapsRepository.GetMapsNamesAsync().Result;
}
