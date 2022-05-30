﻿using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Commands;

public interface IAddMapCommand
{
    Task<ResultModel> AddMapAsync(string mapName, byte[] mapData);

}