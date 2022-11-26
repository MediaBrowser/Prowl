using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using System.IO;
using MediaBrowser.Model.Drawing;
using System.Linq;

namespace MediaBrowser.Plugins.ProwlNotifications
{
    /// <summary>
    /// Class Plugin
    /// </summary>
    public class Plugin : BasePlugin, IHasWebPages, IHasThumbImage, IHasTranslations
    {
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "prowlnotifications",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.prowl.html",
                    EnableInMainMenu = true,
                    MenuIcon = "notifications"
                },
                new PluginPageInfo
                {
                    Name = "prowlnotificationsjs",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.prowl.js"
                },
                new PluginPageInfo
                {
                    Name = "prowlnotificationeditorjs",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.prowleditor.js"
                },
                new PluginPageInfo
                {
                    Name = "prowleditortemplate",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.prowleditor.template.html"
                }
            };
        }

        public TranslationInfo[] GetTranslations()
        {
            var basePath = GetType().Namespace + ".strings.";

            return GetType()
                .Assembly
                .GetManifestResourceNames()
                .Where(i => i.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
                .Select(i => new TranslationInfo
                {
                    Locale = Path.GetFileNameWithoutExtension(i.Substring(basePath.Length)),
                    EmbeddedResourcePath = i

                }).ToArray();
        }

        private Guid _id = new Guid("577f89eb-58a7-4013-be06-9a970ddb1377");
        public override Guid Id
        {
            get { return _id; }
        }

        public static string StaticName = "Prowl Notifications";

        /// <summary>
        /// Gets the name of the plugin
        /// </summary>
        /// <value>The name.</value>
        public override string Name
        {
            get { return StaticName; }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description
        {
            get
            {
                return "Sends notifications via Prowl Service.";
            }
        }

        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".thumb.png");
        }

        public ImageFormat ThumbImageFormat
        {
            get
            {
                return ImageFormat.Png;
            }
        }
    }
}
