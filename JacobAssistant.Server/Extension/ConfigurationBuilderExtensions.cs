using System;
using JacobAssistant.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.VisualBasic;

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

            Console.WriteLine(redis);
            Console.WriteLine(mysql);
            if (!string.IsNullOrEmpty(redis))
            {
                return builder.Add(new RedisConfigurationSource(redis));
            }

            /*
            if (!string.IsNullOrEmpty(mysql))
                return builder.Add();*/

            return builder;
        }
    }
}