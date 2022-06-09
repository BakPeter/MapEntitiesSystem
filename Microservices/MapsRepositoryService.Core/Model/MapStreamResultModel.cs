namespace MapsRepositoryService.Core.Model;

public record MapStreamResultModel(bool Success, string ErrorMessage = "", Stream? Stream = null);