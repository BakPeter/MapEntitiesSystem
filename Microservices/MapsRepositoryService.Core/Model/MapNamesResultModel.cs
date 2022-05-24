namespace MapsRepositoryService.Core.Model;

public record MapNamesResultModel(bool Success, IReadOnlyCollection<string> MapsNames, string ErrorMessage = "");