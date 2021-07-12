using System.Collections.Generic;
using JacobAssistant.Common.Models;

namespace JacobAssistant.Common.Configuration
{
    public class RedisSettingManager:ISettingsManager
    {
        
        public IEnumerable<Config> GetAllConfigs()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> GetAllConfigsDict()
        {
            throw new System.NotImplementedException();
        }

        public Config GetConfig(string name)
        {
            throw new System.NotImplementedException();
        }

    }
}