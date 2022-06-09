using MapsRepositoryService.Core.Model;
using MapsRepositoryService.Core.Repository.Maps.Commands;
using MapsRepositoryService.Core.Repository.Maps.Queries;
using MapsRepositoryService.Core.Repository.MissionMap.Commands;
using MapsRepositoryService.Core.Repository.MissionMap.Queries;

namespace MapsRepositoryService.Core.Repository;
public class MapsRepository : IMapsRepository
{
    private readonly IGetMapBase64Query _getMapBase64Query;
    private readonly IGetMapsNamesQuery _getMapsNamesQuery;
    private readonly IAddMapCommand _addMapCommand;
    private readonly IDeleteMapCommand _deleteMapCommand;
    private readonly IIsMapNameUniqueQuery _isMapNameUniqueQuery;
    private readonly IGetMissionMapQuery _getMissionMapQuery;
    private readonly IAddMissionMapCommand _addMissionMapCommand;
    private readonly IGetMapStreamQuery _getMapStreamQuery;
    private readonly IDeleteMissionMapCommand _deleteMissionMapCommand;

    public MapsRepository(IGetMapBase64Query getMapBase64Query,
                          IGetMapsNamesQuery getMapsNamesQuery,
                          IAddMapCommand addMapCommand,
                          IDeleteMapCommand deleteMapCommand,
                          IIsMapNameUniqueQuery isMapNameUniqueQuery,
                          IGetMissionMapQuery getMissionMapQuery,
                          IAddMissionMapCommand addMissionMapCommand,
                          IGetMapStreamQuery getMapStreamQuery,
                          IDeleteMissionMapCommand deleteMissionMapCommand)
    {
        _getMapBase64Query = getMapBase64Query;
        _getMapsNamesQuery = getMapsNamesQuery;
        _addMapCommand = addMapCommand;
        _deleteMapCommand = deleteMapCommand;
        _isMapNameUniqueQuery = isMapNameUniqueQuery;
        _getMissionMapQuery = getMissionMapQuery;
        _addMissionMapCommand = addMissionMapCommand;
        _getMapStreamQuery = getMapStreamQuery;
        _deleteMissionMapCommand = deleteMissionMapCommand;
    }

    public Task<MapResultModel> GetMapDataAsync(string mapName) => _getMapBase64Query.GetMapBase64Async(mapName);

    public Task<MapNamesResultModel> GetMapsNamesAsync() => _getMapsNamesQuery.GetMapsNamesAsync();

    public Task<ResultModel> AddMapAsync(MapModel mapModel) => _addMapCommand.AddMapAsync(mapModel);

    public Task<ResultModel> DeleteMapAsync(string mapName) => _deleteMapCommand.DeleteMapAsync(mapName);

    public IsMapNameUniqResultModel IsMapNameUnique(MapNameModel mapNameModel) => _isMapNameUniqueQuery.IsMapNameUnique(mapNameModel);

    public async Task<MapResultModel> GetMissionMapBase64Async() => await _getMissionMapQuery.GetMissionMapBase64Async();

    public Task<ResultModel> SetMissionMapAsync(string mapName) => _addMissionMapCommand.AddMissionMapAsync(mapName);
   
    public Task<MapStreamResultModel> GetMapStream(string mapName) => _getMapStreamQuery.GetMapStream(mapName);
    
    public Task<ResultModel> DeleteMissionMap() => _deleteMissionMapCommand.DeleteMissionMap();
}
