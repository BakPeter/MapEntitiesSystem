﻿namespace MapsRepositoryService.Core.Model;

public class MapNamesResultModel
{
    public bool Success { get; set; }
    public List<string> MapsNames { get; set; } = new();
    public string ErrorMessage { get; set; } = string.Empty;
}
