using JacobAssistant.Common;
using NUnit.Framework;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class RedisClientTest
    {
        [Test]
        public void TestStart()
        {
            var redis = new RedisClient();
            redis.Start();
        }
    }
}