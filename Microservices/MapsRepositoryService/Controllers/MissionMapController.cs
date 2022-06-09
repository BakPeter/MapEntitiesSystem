using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapsRepositoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class MissionMapController : Controller
{
    private readonly IMissionMapService _missionMapService;

    public MissionMapController(IMissionMapService missionMapService)
    {
        _missionMapService = missionMapService;
    }

    [HttpGet]
    public async Task<MapResultModel> Get() => await _missionMapService.GetMissionMapBase64Async();

    [HttpPost]
    public async Task<ResultModel> Post([FromForm] string mapName) => await _missionMapService.SetMissionMapAsync(mapName);
}