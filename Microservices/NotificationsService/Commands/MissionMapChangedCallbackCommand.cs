using MessageBroker.Core.Models;
using Microsoft.AspNetCore.SignalR;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;
using NotificationsService.Hubs;

namespace NotificationsService.Commands;

internal class MissionMapChangedCallbackCommand : IMissionMapChangedCallbackCommand
{
    private readonly IHubContext<ServiceHub> _missionMapHubContext;
    private readonly Settings _settings;

    public MissionMapChangedCallbackCommand(IHubContext<ServiceHub> missionMapHubContext,
        Settings settings)
    {
        _missionMapHubContext = missionMapHubContext;
        _settings = settings;
    }

    public MessageBrokerResultModel MissionMapChanged(string missionMapName)
    {
        try
        {
            _missionMapHubContext.Clients.All.SendAsync(_settings.MissionMapNameMethod, missionMapName);
            return new MessageBrokerResultModel(true);
        }
        catch (Exception ex)
        {
            return new MessageBrokerResultModel(false, ex.Message);
        }
    }
}