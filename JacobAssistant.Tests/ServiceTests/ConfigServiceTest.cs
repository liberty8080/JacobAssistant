using System;
using JacobAssistant.Models;
using JacobAssistant.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace JacobAssistant.Tests.ServiceTests
{
    public class ConfigServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ConfigServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Can_get_item()
        {
            using var context = new AssistantDbContext();
            var configService = new ConfigService(context,true);
            var allConfig = configService.GetAll();
            Assert.NotEmpty(allConfig);
            foreach (var config in allConfig)
            {
                _testOutputHelper.WriteLine(config.Value);
            }
        }
    }
}