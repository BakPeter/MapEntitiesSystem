using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Commands;
using MapsRepositoryService.Core.Repository.Queries;

namespace MapsRepositoryService.Core.Repository;
public class MapsRepository : IMapsRepository
{
    private readonly IGetMapDataQuery _getMapDataQuery;
    private readonly IGetMapsNamesQuery _getMapsNamesQuery;
    private readonly IAddMapCommand _addMapCommand;
    private readonly IDeleteMapCommand _deleteMapCommand;
    private readonly IIsMapNameUniqQuery _isMapNameUniqQuery;

    public MapsRepository(IGetMapDataQuery getMapDataQuery,
                          IGetMapsNamesQuery getMapsNamesQuery,
                          IAddMapCommand addMapCommand,
                          IDeleteMapCommand deleteMapCommand,
                          IIsMapNameUniqQuery isMapNameUniqQuery)
    {
        _getMapDataQuery = getMapDataQuery;
        _getMapsNamesQuery = getMapsNamesQuery;
        _addMapCommand = addMapCommand;
        _deleteMapCommand = deleteMapCommand;
        _isMapNameUniqQuery = isMapNameUniqQuery;
    }

    public Task<MapResultModel> GetMapDataAsync(string mapName) => _getMapDataQuery.GetMapByNameAsync(mapName);

    public Task<MapNamesResultModel> GetMapsNamesAsync() => _getMapsNamesQuery.GetMapsNamesAsync();

    public Task<ResultModel> AddMapAsync(MapModel mapModel) => _addMapCommand.AddMapAsync(mapModel);

    public Task<ResultModel> DeleteMapAsync(string mapName) => _deleteMapCommand.DeleteMapAsync(mapName);

    public Task<IsMapNameUniqResultModel> IsMapNameExits(MapNameModel mapNameModel)
      =>  _isMapNameUniqQuery.IsMapNameUniq(mapNameModel);
}
