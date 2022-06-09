using MessageBroker.Core;
using MessageBroker.Infrastructure.Interfaces;
using MessageBroker.Infrastructure.RabbitMq;
using MessageBroker.Infrastructure.RabbitMq.Builder;
using MessageBroker.Infrastructure.RabbitMq.Builder.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBroker.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddMessageBrokerPubSubServices(this IServiceCollection services, RabbitMqConfiguration configuration)
    {
        services.AddScoped(_ => new RabbitMqChannelBuilder(configuration));
        services.AddScoped<IPublisher, Publisher>();
        services.AddScoped<IPublisherAdapter, PublisherRabbitMqAdapter>();
        services.AddScoped<ISubscriber, Subscriber>();
        services.AddScoped<ISubscriberAdapter, SubscriberRabbitMqAdapter>();
    }
}