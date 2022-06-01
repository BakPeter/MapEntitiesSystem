using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Commands;

public interface IDeleteMapCommand
{
    Task<ResultModel> DeleteMapAsync(string mapName);
}
