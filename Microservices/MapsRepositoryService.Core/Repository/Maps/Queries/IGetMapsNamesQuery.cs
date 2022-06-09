using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Maps.Queries;

public interface IGetMapsNamesQuery
{
    Task<MapNamesResultModel> GetMapsNamesAsync();
}
