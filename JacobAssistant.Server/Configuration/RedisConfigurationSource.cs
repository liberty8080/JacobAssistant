using System;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Configuration;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace JacobAssistant.Configuration
{
    /// <summary>
    /// redis 配置源
    /// todo: 修改为options实现
    /// </summary>
    public class RedisConfigurationSource : IConfigurationSource
    {
        private readonly string _connStr;

        public RedisConfigurationSource(string connStr)
        {
            _connStr = connStr;
        }

        /// <summary>
        /// 返回 redis provider
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new RedisConfigurationProvider(_connStr);
        }
    }

    /// <summary>
    /// redis provider
    /// </summary>
    public class RedisConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connectionStr;
        private const string RedisConfigurationDbKey = "Configuration";

        public RedisConfigurationProvider(string connectionStr)
        {
            _connectionStr = connectionStr;
        }

        /// <summary>
        /// 重写load方法,加载配置信息到内存
        /// </summary>
        public override void Load()
        {
            using var redis = ConnectionMultiplexer.Connect(_connectionStr);
            var conf = redis.GetDatabase(0).HashGetAll(RedisConfigurationDbKey);
            Data = conf.Any() ? conf.ToStringDictionary() : CreateAndSaveDefaultValues(redis.GetDatabase(0));
        }


        /// <summary>
        /// 创建默认配置,并返回;
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        static Dictionary<string, string> CreateAndSaveDefaultValues(IDatabase db)
        {
            var result = ConfigMapping.AllConfigKeys()
                .ConvertAll(e => new HashEntry(e, ""))
                .ToArray();

            db.HashSet(RedisConfigurationDbKey, result);
            return result.ToDictionary(item => item.Name.ToString(), item => item.Value.ToString());
        }
    }
}