using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;

namespace MapsRepositoryService.Core.Services;
public class MapsRepository : IMapsRepository
{
    private readonly IGetMapDataQuery _getMapDataQuery;

    public MapsRepository(IGetMapDataQuery getMapDataQuery)
    {
        _getMapDataQuery = getMapDataQuery;
    }

    public MapResultModel GetMapData(string mapName)
    {
        return _getMapDataQuery.GetMapData(mapName);
    }
}
