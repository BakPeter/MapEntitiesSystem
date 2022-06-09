using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Maps.Commands;

public interface IDeleteMapCommand
{
    Task<ResultModel> DeleteMapAsync(string mapName);
}
