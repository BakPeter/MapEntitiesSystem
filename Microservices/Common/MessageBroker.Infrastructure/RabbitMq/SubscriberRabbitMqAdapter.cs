using System.Text;
using MessageBroker.Infrastructure.Interfaces;
using MessageBroker.Infrastructure.RabbitMq.Builder;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBroker.Infrastructure.RabbitMq;

internal class SubscriberRabbitMqAdapter : ISubscriberAdapter, IDisposable
{
    private readonly IModel _channel;

    public SubscriberRabbitMqAdapter(RabbitMqChannelBuilder rabbitMqChannelBuilder)
    {
        _channel = rabbitMqChannelBuilder.Build();
    }

    public void Subscribe(string topic, Action<string>? callBack, CancellationToken cancellationToken)
    {
        try
        {
            _channel.QueueDeclare(queue: topic, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                callBack?.Invoke(message);
            };

            _channel.BasicConsume(queue: topic, autoAck: true, consumer: consumer);
        }
        catch (Exception ex)
        {
            throw ex switch
            {
                OperationCanceledException => new Exception("Operation was canceled."),
                _ => new Exception(ex.Message)
            };
        }
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}