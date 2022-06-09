using MessageBroker.Core.Models;

namespace MessageBroker.Infrastructure.Interfaces;

internal interface IPublisherAdapter
{
    MessageBrokerResultModel Publish(string topic, string message);
}