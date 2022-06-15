namespace NotificationsService.Configurations;

public class Settings
{
    public string BrokerName { get; set; } = string.Empty;
    public string MissionMapTopic { get; set; } = string.Empty;
    public string EntitiesTopic { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string MissionMapNameMethod { get; set; } = string.Empty;
    public string MapEntitiesNameMethod { get; set; } = string.Empty;
}