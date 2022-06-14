namespace NotificationsService.Configurations;

public class Settings
{
    public string MissionMapTopic { get; set; } = string.Empty;
    public string EntitiesTopic { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string MissionMapMethodName { get; set; } = string.Empty;
    public string MapEntitiesMethodName { get; set; } = string.Empty;
}