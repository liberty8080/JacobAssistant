using System;
using JacobAssistant.Common.Configuration;
using NUnit.Framework;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class ConfigMappingTest
    {
        [Test]
        public void AllConfigKeys()
        {
            Assert.Contains("Telegram_DevBotToken", ConfigMapping.AllConfigKeys());
        }
    }
}