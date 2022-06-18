using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapEntitiesService.Controllers;

[ApiController]
[Route("[controller]")]
public class MapEntityController : Controller
{
    private readonly IMapEntityService _mapEntityService;

    public MapEntityController(IMapEntityService mapEntityService) => _mapEntityService = mapEntityService;

    [HttpPost]
    public async Task<ResultModel> Post([FromForm] MapEntityModel mapEntityModel) =>
        await _mapEntityService.HandleMapEntityAsync(mapEntityModel);
}