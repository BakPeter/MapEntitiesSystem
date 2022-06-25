using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Ocelot.json");

builder.Services.AddOcelot();
<<<<<<< HEAD
=======
builder.Services.AddCors();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});
>>>>>>> 4e772cfaf017ea32c9c9aebaee86be71a30c2c8d

var app = builder.Build();
app.UseWebSockets();
await app.UseOcelot();

app.Run();
