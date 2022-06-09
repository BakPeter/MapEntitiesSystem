using MessageBroker.Core.Models;

namespace MessageBroker.Core;

public interface IPublisher
{
    MessageBrokerResultModel Publish(string topic, string message);
}
