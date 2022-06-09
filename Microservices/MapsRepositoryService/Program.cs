using MapsRepositoryService.Core.Configuration;
using MapsRepositoryService.Infrastructure;
using MessageBroker.Infrastructure;
using MessageBroker.Infrastructure.RabbitMq.Builder.Configurations;

var builder = WebApplication.CreateBuilder(args);

var repositorySettings = builder.Configuration.GetSection("MapsRepositorySettings").Get<Settings>();
var messageBrokerSettings = builder.Configuration.GetSection("MessageBrokerSettings").Get<MessageBrokerSettings>();

builder.Services.AddMapsRepositoryServiceInfrastructureServices(repositorySettings, messageBrokerSettings);
builder.Services.AddMessageBrokerPubSubServices(new RabbitMqConfiguration { HostName = messageBrokerSettings.HostName});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();