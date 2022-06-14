using MessageBroker.Infrastructure;
using MessageBroker.Infrastructure.RabbitMq.Builder.Configurations;
using Microsoft.AspNetCore.Http.Connections;
using NotificationsService.Commands;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;
using NotificationsService.Hubs;
using NotificationsService.Services;

var builder = WebApplication.CreateBuilder(args);
var messageBrokerSettings = builder.Configuration.GetSection("MessageBrokerSettings").Get<MessageBrokerSettings>();
var missionMapHubSettings = builder.Configuration.GetSection("MissionMapHubSettings").Get<MissionMapHubSettings>();

builder.Services.AddSignalR();

builder.Services.AddHostedService<WorkerService>();
builder.Services.AddMessageBrokerPubSubServices(new RabbitMqConfiguration { HostName = messageBrokerSettings.HostName });
builder.Services.AddScoped<IMissionMapChangedCallbackCommand, MissionMapChangedCallbackCommand>();
builder.Services.AddScoped(_ => new Settings
{
    Topic = messageBrokerSettings.MissionMapTopic,
    Url = missionMapHubSettings.Url,
    MissionMapNameMethod = missionMapHubSettings.MissionMapNameMethod
});

var app = builder.Build();

app.MapHub<ServiceHub>("/", config =>
{
    config.Transports = HttpTransportType.WebSockets;
});

app.Run();
