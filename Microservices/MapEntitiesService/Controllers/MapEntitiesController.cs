using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapEntitiesService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MapEntitiesController : Controller
    {
        private readonly IMapEntityService _mapEntityService;

        public MapEntitiesController(IMapEntityService mapEntityService)
        {
            _mapEntityService = mapEntityService;
        }


        [HttpPost]
        public async Task<ActionResult<string>> HandleEntity([FromBody] Entity entity)
        {
            var result = await _mapEntityService.HandleEntityAsync(entity);

            return Ok(result);
            //if (result == true)
            //{
            //    return Ok(result);
            //}
            //else
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, result);
            //}
        }
    }
}
