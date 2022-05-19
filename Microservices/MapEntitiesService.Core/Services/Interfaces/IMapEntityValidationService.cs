using MapEntitiesService.Core.Model;

namespace MapEntitiesService.Core.Services.Interfaces
{
    public interface IMapEntityValidationService
    {
        ResultModel Validate(MapEntityModel entityModel);
    }
}
