using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapsRepositoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class MapsController : Controller
{
    private readonly ILogger<MapsController> _logger;
    private readonly IMapsService _mapsRepositoryService;
    public record MapPostModel(string MapName, IFormFile File);

    public MapsController(
        ILogger<MapsController> logger,
        IMapsService mapsRepositoryService)
    {
        _logger = logger;
        _mapsRepositoryService = mapsRepositoryService;
    }

    [HttpGet]
    public MapNamesResultModel Get() => _mapsRepositoryService.GetMapsNames();
    
    [HttpGet("{mapName}")]
    public MapResultModel Get([FromRoute] string mapName) => _mapsRepositoryService.GetMapData(mapName);

    [HttpPost]
    public async Task<ResultModel> Post([FromForm] MapPostModel mapPostModel)
    {
        try
        {
            var extension = Path.GetExtension(mapPostModel.File.FileName);
            var modelDto = new MapModel(Name: mapPostModel.MapName, Extension: extension, Data: mapPostModel.File.OpenReadStream());
            return await _mapsRepositoryService.AddMap(modelDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
        
    }

    [HttpDelete]
    public ResultModel Delete(string mapName) => _mapsRepositoryService.DeleteMap(mapName);
}
