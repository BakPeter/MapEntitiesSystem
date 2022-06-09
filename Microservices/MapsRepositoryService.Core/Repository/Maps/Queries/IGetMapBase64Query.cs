using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Maps.Queries;

public interface IGetMapBase64Query
{
    Task<MapResultModel> GetMapBase64Async(string mapName);
}