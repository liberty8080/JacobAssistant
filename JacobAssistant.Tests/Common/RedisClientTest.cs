using JacobAssistant.Common;
using JacobAssistant.Services;
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

        [Test]
        public void wakeOnlanTest()
        {
            WakeOnLanService.WakeUp("A8A15932CD04", 33, "192.168.1.12");
        }
    }
}