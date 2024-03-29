﻿using MessageBroker.Core.Models;
using Microsoft.AspNetCore.SignalR;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;
using NotificationsService.Hubs;

namespace NotificationsService.Commands;
internal class MapEntitySendCallbackCommand : IMapEntitySendCallbackCommand
{
    private readonly IHubContext<ServiceHub> _missionMapHubContext;
    private readonly Settings _settings;
    private readonly ILogger<MapEntitySendCallbackCommand> _logger;

    public MapEntitySendCallbackCommand(
        IHubContext<ServiceHub> missionMapHubContext,
        Settings settings,
        ILogger<MapEntitySendCallbackCommand> logger)
    {
        _missionMapHubContext = missionMapHubContext;
        _settings = settings;
        _logger = logger;
    }

    public MessageBrokerResultModel EntityPublished(string entity)
    {
        try
        {
            _logger.LogInformation(
                "Services project: service={service}, method={method}, dto={dto}",
                "MapEntitySendCallbackCommand",
                "EntityPublished",
                entity);
            //_logger.LogInformation("Mission map published: {missionMapName}", missionMapName);
            _missionMapHubContext.Clients.All.SendAsync(_settings.MapEntitiesNameMethod, entity);
            return new MessageBrokerResultModel(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "errorMessage: {errorMessage}", ex.Message);
            return new MessageBrokerResultModel(false, "Map Entity sync failed");
        }
    }
}
