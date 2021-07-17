using JacobAssistant.Configuration;
using NUnit.Framework;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class MysqlConfigurationProviderTest
    {
        private DbConfigurationProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = new DbConfigurationProvider("server=192.168.1.12;userid=aide;pwd=jacob_aide;port=3306;database=jacob_aide;sslmode=none");
        }

        [Test]
        public void LoadTest()
        {
            _provider.Load();
        }
    }
}