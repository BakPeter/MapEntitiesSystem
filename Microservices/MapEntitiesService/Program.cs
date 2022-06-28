using MapEntitiesService.Core.Configurations;
using MapEntitiesService.Infrastructure;
using MessageBroker.Infrastructure;
using MessageBroker.Infrastructure.RabbitMq.Builder.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("MessageBrokerSettings").Get<Settings>();

builder.Services.AddMapEntitiesServiceInfrastructureServices(settings);
builder.Services.AddMessageBrokerPubSubServices(new RabbitMqConfiguration { HostName = settings.HostName });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Serlog

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
