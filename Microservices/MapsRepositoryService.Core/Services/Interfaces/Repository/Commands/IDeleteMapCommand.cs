using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;

    public interface IDeleteMapCommand
    {
    Task<ResultModel> DeleteMapAsync(string mapName);

}
