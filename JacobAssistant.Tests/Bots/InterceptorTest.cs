using System;
using JacobAssistant.Bots.Interceptors;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Common.Models;
using JacobAssistant.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Telegram.Bot.Types;

namespace JacobAssistant.Tests.Bots
{
    [TestFixture]
    public class InterceptorTest:BaseTest
    {
        [Test]
        public void PreHandleTest()
        {
            var request = new BotMsgRequest(new Message
            {
                Text = "/help test",
                Date = DateTime.Now,
                MessageId = 1,
                From = new User {Username = "jacob", Id = 111L}
            });
            var dbMock = Mock.Of<ConfigurationDbContext>();
            var service = new Mock<PermissionService>(dbMock);
            service
                .Setup(permissionService =>
                    permissionService.CheckPermission(It.IsAny<BotUser>(), It.IsAny<string>()))
                .Returns(false);
            var handler = new PermissionInterceptor(service.Object);
            var response = new BotMsgResponse {Text = null};
            var result = handler.PreHandle(ref request, ref response);
            Assert.False(result);
            Assert.NotNull(response.Text);
        }

        [Test]
        public void InterceptorChainTest()
        {
            var dbMock = Mock.Of<ConfigurationDbContext>();
            var service = new Mock<PermissionService>(dbMock);
            service
                .Setup(permissionService =>
                    permissionService.CheckPermission(It.IsAny<BotUser>(), It.IsAny<string>()))
                .Returns(false);
            IMsgInterceptor[] msgInterceptors = {new PermissionInterceptor(service.Object)};

            var chain = new InterceptorChain(msgInterceptors);
            var request = new BotMsgRequest
            {
                    Content = "/help ",
                    Time = DateTime.Now,
                    From = new BotUser(),
                    MsgId = "111"
            };
            var response = new BotMsgResponse();
            var result = chain.ApplyPreHandle(ref request,ref response);
            Assert.False(result);
        }
    }
}