namespace MissionMapClient.Configurations;

public class Settings
{
   public HubSettings MissionMapHubSettings { get; set; }
}

public class HubSettings
{
    public string Url { get; set; } = string.Empty;
    public string MapEntitiesNameMethod { get; set; } = string.Empty;
    public string MissionMapNameMethod { get; set; } = string.Empty;
}