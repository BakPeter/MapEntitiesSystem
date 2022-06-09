namespace MapsRepositoryService.Core.Model;

public record IsMapNameUniqResultModel(bool Success, bool NameUnique = false, string ErrorMessage = "");
