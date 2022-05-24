namespace MapsRepositoryService.Core.Model;

public record MapModel(string Name = "", string Extension = "", Stream? Data = null);
