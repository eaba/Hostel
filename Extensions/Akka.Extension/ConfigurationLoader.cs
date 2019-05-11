using Akka.Configuration;
using System.IO;

namespace Akka.Extension
{
    public class ConfigurationLoader
    {
        public static Config Load(string hoconPath) => LoadConfig(hoconPath);

        private static Config LoadConfig(string configFile)
        {
            if (File.Exists(configFile))
            {
                string config = File.ReadAllText(configFile);
                return ConfigurationFactory.ParseString(config);
            }

            return Config.Empty;
        }
    }
}
