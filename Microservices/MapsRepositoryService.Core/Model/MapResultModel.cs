namespace MapsRepositoryService.Core.Model;

public record MapResultModel(bool Success, MapModel MapModel, string ErrorMessage = "");
