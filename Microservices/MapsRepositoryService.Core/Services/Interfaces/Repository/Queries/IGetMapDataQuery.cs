using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;

public interface IGetMapDataQuery
{
    Task<MapResultModel> GetMapByNameAsync(string mapName);
}