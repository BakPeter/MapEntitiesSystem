using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Maps.Queries;

public interface IGetMapStreamQuery
{
    Task<MapStreamResultModel> GetMapStream(string mapName);
}