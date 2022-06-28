using MapPresenterApplication.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MapPresenterApplication;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        _host = Host.CreateDefaultBuilder()
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext();
            })
            .ConfigureServices((_, services)
                => ServiceCollectionConfigurationHelper.ConfigureServices(services, config))
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }
    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }
        base.OnExit(e);
    }
}