using System.Reflection;
using JacobAssistant.Bot;
using JacobAssistant.Models;
using JacobAssistant.Services;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using Xunit.Abstractions;

namespace JacobAssistant.Tests.BotTests
{
    public class BotClientTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BotClientTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestClient()
        {
            var configService = new ConfigService(new AssistantDbContext(), true);

            var client = new AssistantBotClient(configService.BotOptions(), null);
            Assert.NotNull(client);
        }


        [Fact]
        public void TestMatchCommand()
        {
            var msg = "help";
            Assert.Equal("help", AssistantBotClient.MatchCommand(msg).GetCustomAttribute<Cmd>()?.Name);
        }
    }
}