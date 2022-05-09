using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Services.Interfaces
{
    public interface IMapEntityService
    {
        Task<bool> HandleEntity(Entity entity);
    }
}
