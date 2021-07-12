using System.Collections.Generic;
using JacobAssistant.Common.Models;

namespace JacobAssistant.Common.Configuration
{
    public class DefaultConfigProvider:IConfigProvider
    {
        public ISettingsManager Manager { get; set; }
        public IEnumerable<Config> GetAllConfigs()
        {
            return Manager.GetAllConfigs();
        }

        public Config GetConfig(string name)
        {
            return Manager.GetConfig(name);
        }
    }
}