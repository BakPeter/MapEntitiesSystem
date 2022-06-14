using MessageBroker.Core.Models;
using Microsoft.AspNetCore.SignalR;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Hubs;

namespace NotificationsService.Commands;

internal class MissionMapChangedCallbackCommand: IMissionMapChangedCallbackCommand
{
    private readonly IHubContext<MissionMapHub> _missionMapHubContext;

    public MissionMapChangedCallbackCommand(IHubContext<MissionMapHub> missionMapHubContext)
    {
        _missionMapHubContext = missionMapHubContext;
    }

    public MessageBrokerResultModel MissionMapChanged(string missionMapName)
    {
        _missionMapHubContext.Clients.All.SendAsync("", missionMapName);
        throw new NotImplementedException();
    }
}