using JacobAssistant.Configuration;
using NUnit.Framework;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class RedisConfigurationProviderTest
    {
        [Test]
        public void ProviderTest()
        {
            
            RedisConfigurationProvider provider = new("192.168.1.12:6379");
            provider.Load();
            
        }
    }
}