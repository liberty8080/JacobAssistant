using System;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Configuration;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace JacobAssistant.Configuration
{
    
    
    public class RedisConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RedisConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connectionStr;
        private const string RedisConfigurationDbKey = "Configuration";

        public RedisConfigurationProvider(string connectionStr)
        {
            _connectionStr = connectionStr;
        }
        
        public override void Load()
        {
            using var redis = ConnectionMultiplexer.Connect(_connectionStr);
            var conf = redis.GetDatabase(0).HashGetAll(RedisConfigurationDbKey);
            Data = conf.Any() ? conf.ToStringDictionary() : CreateAndSaveDefaultValues(redis.GetDatabase(0));
            foreach (var keyValuePair in Data) Console.WriteLine(keyValuePair);
            }

        /// <summary>
        /// 创建默认配置,并返回;
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        static Dictionary<string,string> CreateAndSaveDefaultValues(IDatabase db)
        {
            var result = ConfigMapping.AllConfigKeys()
                .ConvertAll(e=>new HashEntry(e,""))
                .ToArray();
 
            db.HashSet(RedisConfigurationDbKey,result);
            return result.ToDictionary(item=>item.Name.ToString(),item=>item.Value.ToString());
        }
    }
    
}