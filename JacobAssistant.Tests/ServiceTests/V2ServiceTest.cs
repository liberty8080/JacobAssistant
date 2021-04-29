using System;
using JacobAssistant.Models;
using JacobAssistant.Services;
using Xunit;
using Xunit.Abstractions;

namespace JacobAssistant.Tests.ServiceTests
{
    public class V2ServiceTest
    {
        private readonly ConfigService _configService;
        private readonly ITestOutputHelper _testOutputHelper;

        public V2ServiceTest( ITestOutputHelper testOutputHelper)
        {
            _configService = new ConfigService(new AssistantDbContext(),true);
            _testOutputHelper = testOutputHelper;
        }
        
        

        [Fact]
        public void V2SubDataTest()
        {
            V2Service service= new V2Service(_configService.V2SubLink());
            Assert.NotNull(service.GetV2Sub());
            foreach (var vmess in service.DecryptSub(service.GetV2Sub()))
            {
                _testOutputHelper.WriteLine(vmess.ToString());
            }
        }

        [Fact]
        public void ExpireTest()
        {
            V2Service service= new V2Service(_configService.V2SubLink());
            Assert.Contains("过期", service.Expire());
        }
    }
}