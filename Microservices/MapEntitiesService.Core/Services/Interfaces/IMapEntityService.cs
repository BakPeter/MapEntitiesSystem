using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Services.Interfaces
{
    public interface IMapEntityService
    {
        Task<string> HandleEntityAsync(Entity entity);
        Task<Entity[]> GetAllEntitiesAsync();
    }
}
