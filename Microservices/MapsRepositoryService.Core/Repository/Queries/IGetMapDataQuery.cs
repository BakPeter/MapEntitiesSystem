using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Queries;

public interface IGetMapDataQuery
{
    Task<MapResultModel> GetMapByNameAsync(string mapName);
}