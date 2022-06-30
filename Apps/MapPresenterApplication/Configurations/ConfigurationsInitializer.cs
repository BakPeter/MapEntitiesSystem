using System;
using Microsoft.Extensions.Configuration;

namespace MapPresenterApplication.Configurations;

internal class ConfigurationsInitializer
{
    public static Settings GetSettings(IConfigurationRoot? configuration)
    {
        return configuration is null ?
            GetDefaultConfigurations() :
            GetCalculatedConfigurations(configuration);
    }

    private static Settings GetCalculatedConfigurations(IConfigurationRoot configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));

        var hubSettings = configuration.GetSection("HubSettings").Get<HubSettings>();
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