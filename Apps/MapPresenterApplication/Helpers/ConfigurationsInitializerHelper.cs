using MapPresenterApplication.Configurations;
using Microsoft.Extensions.Configuration;

namespace MapPresenterApplication.Helpers;

internal class ConfigurationsInitializerHelper
{
    public static Settings GetSettings(IConfigurationRoot? configuration)
    {
        return configuration is null ?
            GetDefaultConfigurations() :
            GetCalculatedConfigurations(configuration);
    }

    private static Settings GetCalculatedConfigurations(IConfigurationRoot configuration)
    {
        var hubSettings = configuration.GetSection("MissionMapHubSettings").Get<HubSettings>();
        return new Settings
        {
            HubSettings = hubSettings,
        };
    }

    private static Settings GetDefaultConfigurations()
    {
        //TODO implement GetDefaultConfigurations
        return null;
    }
}