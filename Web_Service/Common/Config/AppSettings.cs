using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web_Service.Common.Config
{
    /// <summary>
    /// AppSettings
    /// </summary>
    public class AppSettings
    {
        private static AppSettings _instance = new AppSettings();
        public static AppSettings getIns() { return _instance; }

        AppSettingsSection appSettings;

        public AppSettings()
        {
            appSettings = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~").AppSettings;
        }

        /// <summary>
        /// WebAPIUrl
        /// </summary>
        public string WebAPIUrl
        {
            get
            {
                if (appSettings.Settings["WebAPIUrl"] == null) return string.Empty;
                return appSettings.Settings["WebAPIUrl"].Value;
            }
        }

        /// <summary>
        /// WebAPIUrl
        /// </summary>
        public string QybBXApiLogEveryday
        {
            get
            {
                if (appSettings.Settings["QybBXApiLogEveryday"] == null) return string.Empty;
                return appSettings.Settings["QybBXApiLogEveryday"].Value;
            }
        }
    }
}