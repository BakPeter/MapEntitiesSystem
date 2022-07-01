namespace MapPresenterApplication.Configurations;

public class Settings
{
    public HubSettings? HubSettings { get; set; }
}

public class HubSettings
{
    public string? Url { get; set; }
    public string? MissionMapNameMethod { get; set; }
    public string? MapEntitiesNameMethod { get; set; }
}