using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Commands
{
    public interface IDeleteMapCommand
    {
        ResultModel DeleteMap(string mapModel);
    }
}
