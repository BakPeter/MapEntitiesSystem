using MessageBroker.Core.Models;

namespace NotificationsService.Commands.Interfaces;

internal interface IMissionMapChangedCallbackCommand
{
    MessageBrokerResultModel MissionMapChanged(string missionMapName);
}