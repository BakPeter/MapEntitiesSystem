using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Ocelot.json");

builder.Services.AddOcelot();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//    builder => builder
//    .WithOrigins("http://localhost:55555")
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .AllowCredentials()
//    );
//});

var app = builder.Build();

app.UseWebSockets();

await app.UseOcelot();

app.Run();
