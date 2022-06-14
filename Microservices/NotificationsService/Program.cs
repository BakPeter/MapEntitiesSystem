using MessageBroker.Infrastructure;
using MessageBroker.Infrastructure.RabbitMq.Builder.Configurations;
using Microsoft.AspNetCore.Http.Connections;
using NotificationsService.Commands;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;
using NotificationsService.Hubs;
using NotificationsService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddHostedService<WorkerService>();

var messageBrokerSettings = builder.Configuration.GetSection("MessageBrokerSettings").Get<MessageBrokerSettings>();
var missionMapHubSettings = builder.Configuration.GetSection("MissionMapHubSettings").Get<MissionMapHubSettings>();
builder.Services.AddSingleton(_ => new Settings
{
    MissionMapTopic = messageBrokerSettings.MissionMapTopic,
    EntitiesTopic = messageBrokerSettings.MissionMapTopic,
    Url = missionMapHubSettings.Url,
    MissionMapMethodName = missionMapHubSettings.MissionMapNameMethod,
    MapEntitiesMethodName = missionMapHubSettings.MapEntitiesNameMethod
});

builder.Services.AddMessageBrokerPubSubServicesSingelton(new RabbitMqConfiguration { HostName = messageBrokerSettings.HostName });
builder.Services.AddSingleton<IMissionMapChangedCallbackCommand, MissionMapChangedCallbackCommand>();
builder.Services.AddSingleton<IMapEntitySendCallbackCommand, MapEntitySendCallbackCommand>();

var app = builder.Build();

app.MapHub<ServiceHub>(missionMapHubSettings.Url, config =>
{
    config.Transports = HttpTransportType.WebSockets;
});

app.Run();
