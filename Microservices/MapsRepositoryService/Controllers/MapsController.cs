using MapsRepositoryService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapsRepositoryService.Controllers;

public class MapsController : Controller
{
    private readonly IMapsService _mapsRepositoryService;

    public MapsController(IMapsService mapsRepositoryService)
    {
        _mapsRepositoryService = mapsRepositoryService;
    }
}
