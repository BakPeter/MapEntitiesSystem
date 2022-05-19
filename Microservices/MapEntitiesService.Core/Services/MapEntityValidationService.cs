using MapEntitiesService.Core.Model;
using MapEntitiesService.Core.Services.Interfaces;

namespace MapEntitiesService.Core.Services
{
    public class MapEntityValidationService : IMapEntityValidationService
    {
        public ResultModel Validate(MapEntityModel entityModel) =>
            //todo - implement EntityModel validation after defined by David
            new(true);
            //new(false, "Validation error message");
    }
}
