using System.Collections.Generic;
using JacobAssistant.Common.Models;

namespace JacobAssistant.Common.Configuration
{
    public interface ISettingsManager
    {
        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns>Configs</returns>
        IEnumerable<Config> GetAllConfigs();

        Dictionary<string, string> GetAllConfigsDict();

        /// <summary>
        /// 通过名称获取配置
        /// </summary>
        /// <param name="name">配置的原始名称</param>
        /// <returns></returns>
        Config GetConfig(string name);
    }
}