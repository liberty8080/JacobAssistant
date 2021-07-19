using JacobAssistant.Common.Configuration;
using JacobAssistant.Configuration;
using JacobAssistant.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Serilog;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class MysqlConfigurationProviderTest : BaseTest
    {
        private DbConfigurationProvider _provider;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            var tempConf = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json")
                .AddJsonFile("appsettings.Development2.json")
                .Build(); 
            _provider = new DbConfigurationProvider(tempConf.GetConnectionString("Mysql"));
            _configuration = new ConfigurationBuilder()
                .Add(new DbConfigurationSource(tempConf.GetConnectionString("Mysql")))
                .Build();
        }

        [Test]
        public void LoadTest()
        {
            _provider.Load();
        }

        /// <summary>
        /// todo: 配置热更新待实现
        /// </summary>
        [Test]
        public void UpdateTest()
        {
            var option = _configuration.GetSection(AppOptions.App).Get<AppOptions>();
            option.WechatCorpId = "!";
        }

        [Test]
        public void OptionTest()
        {
            _provider.TryGet("Telegram:DevBotToken", out var result);
            Log.Debug($"Result:{result}");
        }
    }
}