using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MapPresenterApplication.Helpers;

internal class ServiceCollectionConfigurationHelper
{
    public static void ConfigureServices(IServiceCollection services, IConfigurationRoot? configuration)
    {
        services.AddSingleton<MainWindow>();

        //services.AddSingleton(settings);
    }
}