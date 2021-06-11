using System;
using System.IO;
using JacobAssistant.Ladder;
using JacobAssistant.Models;
using JacobAssistant.Services;
using Xunit;
using Xunit.Abstractions;

namespace JacobAssistant.Tests.LadderTest
{
    public class FastLinkTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly FastLink _fastLink;

        public FastLinkTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var config = new ConfigService(new AssistantDbContext(),false);
            _fastLink = new FastLink(config.GetConfig("fastlink_email").Value,
                config.GetConfig("fastlink_passwd").Value);
        }


        [Fact]
        public void GetUser()
        {
            _fastLink.Login();
           _fastLink.GetUserPage();
           var result =_fastLink.GetFlow();
           _testOutputHelper.WriteLine(result);
           Assert.NotNull(result);
        }

        [Fact]
        public void GetFlow()
        {
            var text = File.ReadAllText(@"LadderTest\fastlink_user.html");
            var result = _fastLink.GetFlow(text);
            
            _testOutputHelper.WriteLine(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetExpireTime()
        {
            var text = File.ReadAllText(@"LadderTest\fastlink_user.html");
            var result = _fastLink.GetExpireTime(text);
                
            _testOutputHelper.WriteLine(result);
            Assert.NotNull(result);
        }
    }
}