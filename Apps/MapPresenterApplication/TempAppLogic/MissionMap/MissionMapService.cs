using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MapPresenterApplication.TempAppLogic.MissionMap;
public class MissionMapService : IMissionMapService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MissionMapService(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    public async Task<GetMissionMapResultModel?> GetMissionMapAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        var result = await httpClient.GetFromJsonAsync<GetMissionMapResultModel>("http://localhost:55555/maps/missionmap");
        return result;
    }
}