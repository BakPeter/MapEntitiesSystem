using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapsRepositoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class MapsController : Controller
{
    private readonly IMapsService _mapsRepositoryService;
    public record MapPostModel(string MapName, IFormFile File);

    public MapsController(IMapsService mapsRepositoryService) => _mapsRepositoryService = mapsRepositoryService;

    [HttpGet]
    public MapNamesResultModel Get() => _mapsRepositoryService.GetMapsNames();
    
    [HttpGet("{mapName}")]
    public MapResultModel Get([FromRoute] string mapName) => _mapsRepositoryService.GetMapData(mapName);

    [HttpPost]
    public async Task<ResultModel> Post([FromForm] MapPostModel mapPostModel)
    {
        var extension = Path.GetExtension(mapPostModel.File.FileName);
        var modelDto = new MapModel(Name: mapPostModel.MapName, Extension: extension, Data: mapPostModel.File.OpenReadStream());
        return await _mapsRepositoryService.AddMap(modelDto);
    }

    [HttpDelete]
    public ResultModel Delete(string mapName) => _mapsRepositoryService.DeleteMap(mapName);
}
