using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapEntitiesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapEntityController : Controller
    {
        private readonly IMapEntityService _mapEntityService;

        public MapEntityController(IMapEntityService mapEntityService)
        {
            _mapEntityService = mapEntityService;
        }

        [HttpGet]
        public async Task<ActionResult<Entity[]>> GetAllEntities()
        {
            var entities =  await _mapEntityService.GetAllEntitiesAsync();
            return entities;
        }
    }
}
