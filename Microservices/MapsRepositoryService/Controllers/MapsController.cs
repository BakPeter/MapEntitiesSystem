using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapsRepositoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class MapsController : Controller
{
    private readonly ILogger<MapsController> _logger;
    private readonly IMapsService _mapsService;
    public record MapPostModel(string MapName, IFormFile File);

    public MapsController(
        ILogger<MapsController> logger,
        IMapsService mapsService)
    {
        _logger = logger;
        _mapsService = mapsService;
    }

    [HttpGet]
    public async Task<MapNamesResultModel> Get() => await _mapsService.GetMapsNamesAsync();

    [HttpGet("{mapName}")]
    public async Task<MapResultModel> Get([FromRoute] string mapName) => await _mapsService.GetMapBase64Async(mapName);

    [HttpPost]
    public async Task<ResultModel> Post([FromForm] MapPostModel mapPostModel)
    {
        try
        {
            var extension = Path.GetExtension(mapPostModel.File.FileName);
            var modelDto = new MapModel(Name: mapPostModel.MapName, Extension: extension, Data: mapPostModel.File.OpenReadStream());
            return await _mapsService.AddMapAsync(modelDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {errorMessage}", ex.Message);
            return new ResultModel(false, ErrorMessage: "Failed to add map");
        }
    }

    [HttpDelete]
    public async Task<ResultModel> Delete([FromQuery] string mapName) => await _mapsService.DeleteMapAsync(mapName);
}
