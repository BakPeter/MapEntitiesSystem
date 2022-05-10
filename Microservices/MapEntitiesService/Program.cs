using MapEntitiesService.Core.Configurations;
using MapEntitiesService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

builder.Services.AddMapEntitiesServiceInfrastructureServices(settings);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
