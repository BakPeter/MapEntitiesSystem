using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Commands
{
    public interface IAddMapCommand
    {
       ResultModel AddMap(MapModel mapModel);
    }
}
