using JacobAssistant.Common.Configuration;
using JacobAssistant.Extension;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class ConfigurationTest : BaseTest
    {
        private IConfiguration _configuration;
        [SetUp]
        public void SetUp()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development2.json")
                .AddMyConfiguration()
                .Build();
        }
        [Test]
        public void ConfTest()
        {
            
            var conf = _configuration.AsEnumerable();
            foreach (var keyValuePair in conf)
            {
                Log.Debug(keyValuePair.ToString());
            }
        }

        [Test]
        public void OptionsTest()
        {
            var options = _configuration.GetSection(AppOptions.App).Get<AppOptions>();
            Log.Debug($"dev:{options.TelegramDevBotToken}");
            Log.Debug($"longTest:{options.TelegramAnnounceChannelId}");
        }
        
    }
}