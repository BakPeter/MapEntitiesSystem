using MessageBroker.Core.Models;

namespace MessageBroker.Core;

public interface ISubscriber
{
    void Subscribe(string topic, Func<string, MessageBrokerResultModel> callBack, CancellationToken cancellationToken);
}