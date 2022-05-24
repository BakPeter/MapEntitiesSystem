using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;

public  interface IGetMapDataQuery
{
    MapResultModel GetMapData(string mapName);
}
