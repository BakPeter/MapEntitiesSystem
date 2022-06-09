using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Repository.Maps.Queries;

public interface IIsMapNameUniqueQuery
{
    IsMapNameUniqResultModel IsMapNameUnique(MapNameModel mapNameModel);
}
