using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository;
using MapsRepositoryService.Core.Services.Interfaces;
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

    public async Task<MapResultModel> GetMapBase64Async(string mapName) => await _mapsRepository.GetMapDataAsync(mapName);

    public async Task<ResultModel> AddMapAsync(MapModel mapModel)
    {
        var validationResult = _uploadMapValidation.Validate(mapModel);
        if (validationResult.Success is false) return validationResult;
        return await _mapsRepository.AddMapAsync(mapModel);
    }

    public async Task<ResultModel> DeleteMapAsync(string mapName) => await _mapsRepository.DeleteMapAsync(mapName);
    
    public async Task<MapStreamResultModel> GetMapStreamAsync(string mapName) => await _mapsRepository.GetMapStream(mapName);

    public async Task<MapNamesResultModel> GetMapsNamesAsync() => await _mapsRepository.GetMapsNamesAsync();
}
