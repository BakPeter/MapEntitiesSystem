using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Commands;

public interface IAddMapCommand
{
    Task<ResultModel> AddMapAsync(MapModel mapModel);
}
