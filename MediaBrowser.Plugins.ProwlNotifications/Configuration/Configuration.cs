using Emby.Notifications;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Model.Plugins;
using System.Collections.Generic;
using System;

namespace MediaBrowser.Plugins.ProwlNotifications
{
    /// <summary>
    /// Class PluginConfiguration
    /// </summary>
    public class NotificationsConfigurationFactory : IConfigurationFactory
    {
        public IEnumerable<ConfigurationStore> GetConfigurations()
        {
            return new[]
            {
                new ConfigurationStore
                {
                     ConfigurationType = typeof(ProwlNotificationsOptions),
                     Key = "prowlnotifications"
                }
            };
        }
    }
    public static class NotificationsConfigExtension
    {
        public static ProwlNotificationsOptions GetNotificationsOptions(this IConfigurationManager config)
        {
            return config.GetConfiguration<ProwlNotificationsOptions>("prowlnotifications");
        }

        public static NotificationInfo[] GetConfiguredNotifications(this IConfigurationManager config)
        {
            return config.GetNotificationsOptions().Notifications;
        }

        public static void SaveNotificationsConfiguration(this IConfigurationManager config, ProwlNotificationsOptions options)
        {
            config.SaveConfiguration("prowlnotifications", options);
        }
    }

    public class ProwlNotificationsOptions
    {
        public ProwlNotificationInfo[] Notifications { get; set; } = Array.Empty<ProwlNotificationInfo>();
    }

    public class ProwlNotificationInfo : NotificationInfo
    {
        public string Token { get; set; }
    }
}
