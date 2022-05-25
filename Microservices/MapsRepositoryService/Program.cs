using MapsRepositoryService.Core.Configuration;
using MapsRepositoryService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("MapsRepositorySettings").Get<Settings>();

builder.Services.AddMapsRepositoryServiceInfrastructureServices(settings);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
