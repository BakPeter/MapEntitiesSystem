using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository;

public interface IMapsRepository
{
    MapResultModel GetMapData(string mapName);
}
