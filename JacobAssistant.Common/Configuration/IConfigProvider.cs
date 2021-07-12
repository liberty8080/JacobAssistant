using System.Collections.Generic;
using JacobAssistant.Common.Models;

namespace JacobAssistant.Common.Configuration
{
    /// <summary>
    /// 配置文件提供类,提供配置来源的动态切换
    /// </summary>
    public interface IConfigProvider
    {
        
        ISettingsManager Manager { get; set; }

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns>Configs</returns>
        IEnumerable<Config> GetAllConfigs();

        /// <summary>
        /// 通过名称获取配置
        /// </summary>
        /// <param name="name">配置的原始名称</param>
        /// <returns></returns>
        Config GetConfig(string name);

    }
}