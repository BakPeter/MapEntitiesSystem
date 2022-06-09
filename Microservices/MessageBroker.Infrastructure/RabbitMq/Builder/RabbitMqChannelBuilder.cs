using MessageBroker.Infrastructure.RabbitMq.Builder.Configurations;
using RabbitMQ.Client;

namespace MessageBroker.Infrastructure.RabbitMq.Builder
{
    internal class RabbitMqChannelBuilder: IDisposable
    {
        private readonly RabbitMqConfiguration _configuration;
        private IConnection? _connection;

        public RabbitMqChannelBuilder(RabbitMqConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IModel Build()
        {
            var factory = new ConnectionFactory { HostName = _configuration.HostName };
            _connection = factory.CreateConnection();
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
