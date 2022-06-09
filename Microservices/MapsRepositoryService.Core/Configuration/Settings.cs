namespace MapsRepositoryService.Core.Configuration;

public class Settings
{
    public string Endpoint { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string MapsBucket { get; set; } = string.Empty;
    public string MissionMapBucket { get; set; } = string.Empty;
}
