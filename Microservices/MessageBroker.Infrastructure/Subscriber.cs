using MessageBroker.Core;
using MessageBroker.Core.Models;
using MessageBroker.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessageBroker.Infrastructure;

internal class Subscriber : ISubscriber
{
    private readonly ILogger<Subscriber> _logger;
    private readonly ISubscriberAdapter _subscriberAdapter;

    public Subscriber(ILogger<Subscriber> logger, ISubscriberAdapter subscriberAdapter)
    {
        _logger = logger;
        _subscriberAdapter = subscriberAdapter;
    }

    public void Subscribe(string topic, Func<string, MessageBrokerResultModel> consumeMessageHandler,
                          CancellationToken cancellationToken)
    {
        try
        {
            _subscriberAdapter.Subscribe(topic, consumeMessage =>
            {
                var result = consumeMessageHandler.Invoke(consumeMessage);
                if (result.Success is false)
                    _logger.LogError("Fail to process message, {errorMessage}", result.Message);
            }, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Subscriber.Subscribe failed: {errorMessage}", e.Message);
        }
    }
}