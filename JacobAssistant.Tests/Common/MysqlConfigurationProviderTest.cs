using JacobAssistant.Common.Configuration;
using JacobAssistant.Configuration;
using NUnit.Framework;
using Serilog;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class MysqlConfigurationProviderTest:BaseTest
    {
        private DbConfigurationProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = new DbConfigurationProvider(
                "server=127.0.0.1;userid=aide;pwd=jacob_aide;port=3306;database=jacob_aide;sslmode=none");
        }

        [Test]
        public void LoadTest()
        {
            _provider.Load();
        }

        [Test]
        public void UpdateConfigTest()
        {
            _provider.Set("Test","");
            _provider.TryGet("Test", out var before);
            Log.Debug($"before: {before}");
            _provider.Set("Test","Test");
            _provider.TryGet("Test", out var after);
            Log.Debug($"after: {after}");
            Assert.AreEqual("Test",after);
        }
}
}