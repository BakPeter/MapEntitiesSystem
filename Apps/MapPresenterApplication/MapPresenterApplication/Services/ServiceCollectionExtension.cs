using MapPresenterApplication.Configurations;
using MapPresenterApplication.TempAppLogic.MissionMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MapPresenterApplication.Services;

public static class ServiceCollectionExtension
{
    public static void RegisterMapPresenterAppServices(
        this IServiceCollection services, IConfigurationRoot? configuration)
    {
        services.AddSingleton(ConfigurationsInitializer.GetSettings(configuration));
        services.AddHttpClient();

        services.AddSingleton<MainWindow>();
        services.AddScoped<IMissionMapService, MissionMapService>();
    }
}