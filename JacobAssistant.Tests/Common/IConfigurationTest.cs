using JacobAssistant.Extension;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;

namespace JacobAssistant.Tests.Common
{
    [TestFixture]
    public class ConfigurationTest : BaseTest
    {
        [Test]
        public void ConfTest()
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development2.json")
                .AddMyConfiguration()
                .Build();
            var conf = configurationRoot.AsEnumerable();
            foreach (var keyValuePair in conf)
            {
                Log.Debug(keyValuePair.ToString());
            }
        }
    }
}