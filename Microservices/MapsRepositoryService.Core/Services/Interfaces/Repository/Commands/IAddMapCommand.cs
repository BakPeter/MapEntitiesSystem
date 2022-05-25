using MapsRepositoryService.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsRepositoryService.Core.Services.Interfaces.Repository.Commands
{
    public interface IAddMapCommand
    {
        Task<ResultModel> AddMapAsync(string mapName, byte[] mapData);

    }
}
