using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;

namespace MapEntitiesService.Core.Services
{
    public class MapEntityService : IMapEntityService
    {
        public Task<bool> HandleEntity(Entity entity)
        {
            System.Console.WriteLine(entity);
        
            return Task.FromResult(true);
        }
    }
}
