using System.Threading.Tasks;

namespace MapPresenterApplication.TempAppLogic.MissionMap;

public interface IMissionMapService
{
    Task<GetMissionMapResultModel?> GetMissionMapAsync();
}