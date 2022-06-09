namespace MessageBroker.Infrastructure.Interfaces;

internal interface ISubscriberAdapter
{
    void Subscribe(string topic, Action<string>? consumeMessageHandler, CancellationToken cancellationToken);
}