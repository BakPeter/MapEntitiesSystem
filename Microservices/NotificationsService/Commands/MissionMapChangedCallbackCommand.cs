using MessageBroker.Core.Models;
using Microsoft.AspNetCore.SignalR;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;

namespace NotificationsService.Commands;

internal class MissionMapChangedCallbackCommand : IMissionMapChangedCallbackCommand
{
    private readonly IHubContext _missionMapHubContext;
    private readonly Settings _settings;
    private readonly ILogger<MissionMapChangedCallbackCommand> _logger;

    public MissionMapChangedCallbackCommand(IHubContext missionMapHubContext,
        Settings settings, ILogger<MissionMapChangedCallbackCommand> logger)
    {
        _missionMapHubContext = missionMapHubContext;
        _settings = settings;
        _logger = logger;
    }

    public MessageBrokerResultModel MissionMapChanged(string missionMapName)
    {
        try
        {
            _logger.LogInformation(
                "Services project: service={service}, method={method}, dto={dto}",
                "MissionMapChangedCallbackCommand",
                "MessageBrokerResultModel",
                missionMapName);
            // _logger.LogInformation("Mission map published: {missionMapName}", missionMapName);
            _missionMapHubContext.Clients.All.SendAsync(_settings.MissionMapNameMethod, missionMapName);
            return new MessageBrokerResultModel(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: {errorMessage}", ex.Message);
            return new MessageBrokerResultModel(false, "Mission map client sync failed");
        }
    }
}