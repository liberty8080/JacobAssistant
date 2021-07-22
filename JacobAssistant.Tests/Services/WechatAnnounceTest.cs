using JacobAssistant.Common.Configuration;
using JacobAssistant.Services.Announce;
using JacobAssistant.Services.Wechat;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace JacobAssistant.Tests.Services
{
    [TestFixture]
    public class WechatAnnounceTest:BaseTest
    {
        [Test]
        public void AnnounceTest()
        {
            AppOptions appOptions = new() {WechatCorpId = Configuration["App:WechatCorpId"]
                ,WechatAppSecret = Configuration["App:WechatAppSecret"],
                WechatAnnounceChannelId = Configuration["App:WechatAnnounceChannelId"]
                ,WechatAppId = Configuration["App:WechatAppId"]
            };
            var holder = new WechatTokenHolder(Options.Create(appOptions));
            var announceService = new WechatAnnounceService(holder,Options.Create(appOptions));
            announceService.Announce("test");
        }
    }
}