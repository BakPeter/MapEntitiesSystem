using System.Threading.Tasks;

namespace MapPresenterApplication.TempAppLogic.MissionMap;

public interface IMissionMapHandler
{
    Task<GetMissionMapResultModel?> GetMissionMapAsync();
}