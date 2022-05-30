using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Services.Interfaces.Repository;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;
using MapsRepositoryService.Core.Services.Interfaces.Repository.Queries;

namespace MapsRepositoryService.Core.Services;
public class MapsRepository : IMapsRepository
{
    private readonly IGetMapDataQuery _getMapDataQuery;
    private readonly IGetMapsNamesQuery _getMapsNamesQuery;
    private readonly IAddMapCommand _addMapCommand;
    private readonly IDeleteMapCommand _deleteMapCommand;

    public MapsRepository(IGetMapDataQuery getMapDataQuery, 
                            IGetMapsNamesQuery getMapsNamesQuery,
                            IAddMapCommand addMapCommand,
                            IDeleteMapCommand deleteMapCommand)
    {
        _getMapDataQuery = getMapDataQuery;
        _getMapsNamesQuery = getMapsNamesQuery;
        _addMapCommand = addMapCommand;
        _deleteMapCommand = deleteMapCommand;
    }

    public Task<MapResultModel> GetMapDataAsync(string mapName)
    {
        return _getMapDataQuery.GetMapDataAsync(mapName);
    }

    public Task<MapNamesResultModel> GetMapsNamesAsync()
    {
        return _getMapsNamesQuery.GetMapsNamesAsync();
    }
    public Task<ResultModel> AddMapAsync(string mapName, byte[] mapData)
    {
        return _addMapCommand.AddMapAsync(mapName, mapData);
    }

    public Task<ResultModel> DeleteMapAsync(string mapName)
    {
        return _deleteMapCommand.DeleteMapAsync(mapName);
    }

    public MapNamesResultModel GetMapsNames()
    {
        return _getMapsNamesQuery.GetMapsNames();
    }

    public ResultModel AddMap(MapModel mapModel)
    {
        return _addMapCommand.AddMap(mapModel);
    }

    public ResultModel DeleteMap(string mapName)
    {
        return _deleteMapCommand.DeleteMap(mapName);
    }
}
