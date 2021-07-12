using System;
using StackExchange.Redis;

namespace JacobAssistant.Common
{
    public class RedisClient
    {
        public void Start()
        {
            var redis = ConnectionMultiplexer.Connect("192.168.1.12:6379");
            var db = redis.GetDatabase(0);
            var test = db.HashGetAll("test");
            foreach (var hashEntry in test)
            {
                Console.WriteLine(hashEntry);
            }
        }
    }
}