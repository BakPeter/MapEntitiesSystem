using System.Text;
using MessageBroker.Core.Models;
using MessageBroker.Infrastructure.Interfaces;
using MessageBroker.Infrastructure.RabbitMq.Builder;
using RabbitMQ.Client;

namespace MessageBroker.Infrastructure.RabbitMq;

internal class PublisherRabbitMqAdapter : IPublisherAdapter, IDisposable
{
    private readonly IModel _channel;

    public PublisherRabbitMqAdapter(
        RabbitMqChannelBuilder rabbitMqChannelBuilder)
    {
        _channel = rabbitMqChannelBuilder.Build();
    }

    public MessageBrokerResultModel Publish(string topic, string message)
    {
        try
        {
            _channel.QueueDeclare(queue: topic, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: topic, basicProperties: null, body: body);
            return new MessageBrokerResultModel(Success: true);
        }
        catch (Exception ex)
        {
            throw ex switch
            {
                _ => new Exception(ex.Message)
            };
        }
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}