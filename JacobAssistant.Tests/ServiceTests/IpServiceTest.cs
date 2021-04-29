using System;
using JacobAssistant.Services;
using Xunit;
using Xunit.Abstractions;

namespace JacobAssistant.Tests.ServiceTests
{
    public class IpServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public IpServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GetIpTest()
        {
            var ip = IpService.GetPublicIp();
            _testOutputHelper.WriteLine(ip);
            Assert.NotNull(ip);
        }
        
        
        
    }
}