using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;

public interface IGetMapsNamesQuery
{
    Task<MapNamesResultModel> GetMapsNamesAsync();
}
