using System;
using JacobAssistant.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.VisualBasic;
using Serilog;

namespace JacobAssistant.Extension
{
    /// <summary>
    /// 自定义扩展方法,扩展配置文件
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder)
        {
            var tempConfig = builder.Build();
            var redis = tempConfig["ConfigurationSource:Redis"];
            var mysql = tempConfig["ConfigurationSource:Mysql"];
            
            if (!string.IsNullOrEmpty(mysql))
            {
                Log.Information($"使用Db配置源: {mysql}");
                return builder.Add(new DbConfigurationSource(mysql));
            }

            if (!string.IsNullOrEmpty(redis))
            {
                Log.Information("使用Redis配置源");
                return builder.Add(new RedisConfigurationSource(redis));
            }
            
            


            return builder;
        }
    }
}