using System.Collections.Generic;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Notifications;
using MediaBrowser.Model.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Emby.Notifications;
using MediaBrowser.Controller.Configuration;

namespace MediaBrowser.Plugins.ProwlNotifications
{
    public class Notifier : INotifier
    {
        private IServerConfigurationManager _config;
        private ILogger _logger;
        private IHttpClient _httpClient;

        public static string TestNotificationId = "system.prowlnotificationtest";
        public Notifier(IServerConfigurationManager config, ILogger logger, IHttpClient httpClient)
        {
            _config = config;
            _logger = logger;
            _httpClient = httpClient;   
        }

        public string Name
        {
            get { return Plugin.StaticName; }
        }

        public async Task SendNotification(InternalNotificationRequest request, CancellationToken cancellationToken)
        {
            var options = request.Configuration as ProwlNotificationInfo;

            var parameters = new Dictionary<string, string>
            {
                {"apikey", options.Token},
                {"application", "Emby"}
            };

            if (string.IsNullOrEmpty(request.Description))
            {
                parameters.Add("event", request.Title);
            }
            else
            {
                parameters.Add("event", request.Title);
                parameters.Add("description", request.Description);
            }

            _logger.Debug("Prowl to {0} - {1} - {2}", options.Token, request.Title, request.Description);

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = "https://api.prowlapp.com/publicapi/add",
                CancellationToken = cancellationToken
            };

            httpRequestOptions.SetPostData(parameters);

            using (await _httpClient.Post(httpRequestOptions).ConfigureAwait(false))
            {

            }
        }

        public NotificationInfo[] GetConfiguredNotifications()
        {
            return _config.GetConfiguredNotifications();
        }
    }
}
