using JacobAssistant.Bot;
using JacobAssistant.Bot.core;
using JacobAssistant.Models;
using JacobAssistant.Services;
using Xunit;

namespace JacobAssistant.Tests.BotTests
{
    public class BotClientTest
    {
        [Fact]
        public void TestClient()
        {
            var configService = new ConfigService(new AssistantDbContext(),true);
            
            var client = new AssistantBotClient(configService.BotOptions());
            Assert.NotNull(client);
        }
    }
}