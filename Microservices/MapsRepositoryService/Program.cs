using MapsRepositoryService.Core.Configuration;
using MapsRepositoryService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.GetSection("MapsRepositorySettings").Get<Settings>();
builder.Services.AddMapsRepositoryServiceInfrastructureServices(settings);

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