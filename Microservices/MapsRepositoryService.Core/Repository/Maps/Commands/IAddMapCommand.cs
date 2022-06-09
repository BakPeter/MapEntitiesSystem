using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Maps.Commands;

public interface IAddMapCommand
{
    Task<ResultModel> AddMapAsync(MapModel mapModel);
}
