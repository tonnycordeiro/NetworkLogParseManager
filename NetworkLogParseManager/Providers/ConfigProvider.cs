using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Providers
{
    public class ConfigProvider
    {
        public const string APP_SETTINGS_JSON_PATH = "./appsettings.json";

        public static IConfigurationRoot Configuration { get; set; }

        public ConfigProvider(string appSettingJsonPath = APP_SETTINGS_JSON_PATH)
        {
            var cfgBuilder = new ConfigurationBuilder().AddJsonFile(appSettingJsonPath);
            Configuration = cfgBuilder.Build();
        }

        public int ThresholdQueueToParse { get { return Int32.Parse(Configuration["ThresholdQueueToParse"]); } }
        public int ThresholdQueueToWrite { get { return Int32.Parse(Configuration["ThresholdQueueToWrite"]); } }
    }
}
