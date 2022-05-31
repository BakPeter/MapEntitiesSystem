namespace MapsRepositoryService.Core.Model;

public class MapResultModel
{
    public bool Success { get; set; }
    public MapModel MapModel { get; set; } = new MapModel();
    public string ErrorMessage { get; set; } = string.Empty;
}
