using JacobAssistant.Common.Configuration;
using JacobAssistant.Services.Wechat;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Serilog;

namespace JacobAssistant.Tests.Services
{
    [TestFixture]
    public class WechatTokenHolderTest:BaseTest
    {
        [Test]
        public void TokenTest()
        {
            AppOptions appOptions = new() {WechatCorpId = Configuration["App:WechatCorpId"]
                ,WechatAppSecret = Configuration["App:WechatCorpSecret"]};
            var holder = new WechatTokenHolder(Options.Create(appOptions));
            Log.Debug("test");
            Log.Debug(holder.Token);
            Assert.NotNull(holder);
        }
    }
}