namespace MapPresenterApplication.TempAppLogic.MissionMap;

public record GetMissionMapResultModel(bool Success, string MapBase64, string MapName, string ErrorMessage);
