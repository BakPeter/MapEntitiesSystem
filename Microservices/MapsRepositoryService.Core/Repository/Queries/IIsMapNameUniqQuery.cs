using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Queries;

public interface IIsMapNameUniqQuery
{
    Task<IsMapNameUniqResultModel> IsMapNameUniq(string mapName);
}
