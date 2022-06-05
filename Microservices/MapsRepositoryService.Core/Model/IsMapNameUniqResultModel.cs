namespace MapsRepositoryService.Core.Model;

public record IsMapNameUniqResultModel(bool Success, bool NameUniq = false, string ErrorMessage = "");
