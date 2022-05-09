using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;

namespace MapEntitiesService.Core.Services
{
    public class MapEntityService : IMapEntityService
    {
        public async Task<string> HandleEntityAsync(Entity entity)
        {
            var task = await Task.Run(() => "Operation successful");
            return task;
        }

        public Task<Entity[]> GetAllEntitiesAsync()
        {
            var result = Task.Run(() =>
            {
                var entities = new Entity[]
                {
                    new() {Lat = 10,Lon = 20},
                    new()  {Lat = 20,Lon = 40}
                };
                return entities;
            });

            return result;
        }
    }
}
