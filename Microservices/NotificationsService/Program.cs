using MessageBroker.Infrastructure;
using MessageBroker.Infrastructure.RabbitMq.Builder.Configurations;
using Microsoft.AspNetCore.Http.Connections;
using NotificationsService.Commands;
using NotificationsService.Commands.Interfaces;
using NotificationsService.Configurations;
using NotificationsService.Hubs;
using NotificationsService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Serlog

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

builder.Services.AddSignalR();


builder.Services.AddHostedService<WorkerService>();

var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddSingleton(settings);

builder.Services.AddMessageBrokerPubSubServices(new RabbitMqConfiguration { HostName = settings.HostName });
builder.Services.AddSingleton<IMissionMapChangedCallbackCommand, MissionMapChangedCallbackCommand>();
builder.Services.AddSingleton<IMapEntitySendCallbackCommand, MapEntitySendCallbackCommand>();

var app = builder.Build();

app.MapHub<ServiceHub>(settings.Url, config =>
{
    config.Transports = HttpTransportType.WebSockets;
});

app.Run();
