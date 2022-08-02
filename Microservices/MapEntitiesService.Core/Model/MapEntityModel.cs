using System.Text.Json;

namespace MapEntitiesService.Core.Model;

public class MapEntityModel
{
    public string Title { get; set; } = string.Empty;
    public int Lat { get; set; }
    public int Lon { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}