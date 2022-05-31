using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces;
using MapsRepositoryService.Core.Services.Interfaces.Repository;
using MapsRepositoryService.Core.Validation.Interfaces;

namespace MapsRepositoryService.Core.Services;

public class MapsService : IMapsService
{
    private readonly IMapsRepository _mapsRepository;
    private readonly IUploadMapValidation _uploadMapValidation;

    public MapsService(IMapsRepository mapsRepository, IUploadMapValidation uploadMapValidation)
    {
        _mapsRepository = mapsRepository;
        _uploadMapValidation = uploadMapValidation;
    }

    public MapResultModel GetMapData(string mapName) => _mapsRepository.GetMapDataAsync(mapName).Result;

    public async Task<ResultModel> AddMap(MapModel mapModel)
    {
        var validationResult = _uploadMapValidation.Validate(mapModel);
        if (validationResult.Success is false) return validationResult;
        return await _mapsRepository.AddMapAsync(mapModel);
    }

    public ResultModel DeleteMap(string mapName) => _mapsRepository.DeleteMapAsync(mapName).Result;

    public MapNamesResultModel GetMapsNames() => _mapsRepository.GetMapsNamesAsync().Result;
}
