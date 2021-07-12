using System;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Configuration;
using NUnit.Framework;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class RedisConfigurationProviderTest
    {
        private RedisConfigurationProvider _provider;

        [SetUp]
        public void SetUp()
        {
            _provider = new RedisConfigurationProvider("192.168.1.12:6379");
        }

        [Test]
        public void ProviderTest()
        {
            _provider.Load();
        }

        [Test]
        public void TryGetAndSetTest()
        {
            _provider.TryGet(ConfigMapping.TelegramDevBotToken, out var originalValue);
            Console.WriteLine($"originalValue:{originalValue}");
            _provider.Set(ConfigMapping.TelegramDevBotToken, "test");
            _provider.TryGet(ConfigMapping.TelegramDevBotToken, out var testValue);
            Console.WriteLine($"TestValue: {testValue}");
            Assert.AreEqual("test", testValue);
            _provider.Set(ConfigMapping.TelegramDevBotToken, originalValue);
        }
    }
}