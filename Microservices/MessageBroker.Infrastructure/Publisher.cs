using MessageBroker.Core;
using MessageBroker.Core.Models;
using MessageBroker.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessageBroker.Infrastructure;

internal class Publisher : IPublisher
{
    private readonly ILogger<Publisher> _logger;
    private readonly IPublisherAdapter _publisherAdapter;

    public Publisher(ILogger<Publisher> logger, IPublisherAdapter publisherAdapter)
    {
        _logger = logger;
        _publisherAdapter = publisherAdapter;
    }

    public MessageBrokerResultModel Publish(string topic, string message)
    {
        try
        {
            var result = new MessageBrokerResultModel(Success: true, Message: message);
            _publisherAdapter.Publish(topic, message);
            _logger.LogInformation($"Delivery success: {result.Message}");
            return new MessageBrokerResultModel(Success: true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Delivery failed: {e.Message}");
            return new MessageBrokerResultModel(Success: false, Message: e.Message);
        }
    }
}