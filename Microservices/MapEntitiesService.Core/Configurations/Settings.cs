namespace MapEntitiesService.Core.Configurations;

public record Settings
{
    public string BrokerName { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
}