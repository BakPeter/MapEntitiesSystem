using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Queries;

public interface IGetMapsNamesQuery
{
    Task<MapNamesResultModel> GetMapsNamesAsync();
}
