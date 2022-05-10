using MapEntitiesService.Core.DTO;
using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Services.Interfaces
{
    public interface IMapEntityService
    {
        Task<HandleEntityResponse> HandleEntityAsync(HandleEntityRequest handleEntityRequestDto);
        Task<Entity[]> GetAllEntitiesAsync();
    }
}
