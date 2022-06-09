namespace MapsRepositoryService.Core.Configuration;


public record MessageBrokerSettings
{
    public string BrokerName { get; set; } = string.Empty;
    public string MissionMapTopic { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
}