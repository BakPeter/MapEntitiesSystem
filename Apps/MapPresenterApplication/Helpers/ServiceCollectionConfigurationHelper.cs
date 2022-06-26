using MapPresenterApplication.TempAppLogic.MissionMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MapPresenterApplication.Helpers;

internal class ServiceCollectionConfigurationHelper
{
    public static void ConfigureServices(
        IServiceCollection services,
        IConfigurationRoot? configuration)
    {
        services.AddSingleton(ConfigurationsInitializerHelper.GetSettings(configuration));
        services.AddHttpClient();

        services.AddSingleton<MainWindow>();
        services.AddScoped<IMissionMapHandler, MissionMapHandler>();
    }
}