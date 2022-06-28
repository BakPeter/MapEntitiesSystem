namespace MapsRepositoryService.Core.Model;

public class MapResultModel
{
    public bool Success { get; set; }
    public string MapBase64 { get; set; } = string.Empty;
    public string MapName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
