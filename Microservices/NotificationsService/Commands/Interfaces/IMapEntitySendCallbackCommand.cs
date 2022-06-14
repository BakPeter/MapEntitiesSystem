using MessageBroker.Core.Models;

namespace NotificationsService.Commands.Interfaces;

internal interface IMapEntitySendCallbackCommand
{
    MessageBrokerResultModel EntityPublished(string arg);
}
