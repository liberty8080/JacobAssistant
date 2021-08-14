using System;
using JacobAssistant.Bots.Interceptors;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Common.Models;
using JacobAssistant.Services;
using Moq;
using NUnit.Framework;
using Serilog;

namespace JacobAssistant.Tests.Bots
{
    [TestFixture]
    public class MessageHandlersTest : BaseTest
    {


        [Test]
        public void DispatcherPermissionDeniedTest()
        {
            var dbMock = new Mock<ConfigurationDbContext>();
            var service = new Mock<PermissionService>(dbMock.Object);
            service
                .Setup(permissionService =>
                    permissionService.CheckPermission(It.IsAny<BotUser>(), It.IsAny<string>()))
                .Returns(false);
            var permissionInterceptor = new Mock<PermissionInterceptor>(service.Object);
            var dispatcher = new MessageDispatcher(new []{permissionInterceptor.Object});
            var request = new BotMsgRequest {Content = "/help 111"
                ,From  = new BotUser{Id = 1,Type = 1,UserName = "test",UserId = "test"}
                ,MessageSource = MessageSource.Telegram
                ,MsgId = "msgId",
                Time = DateTime.Now
            };

            var response = new BotMsgResponse();
            dispatcher.DoDispatch(ref request,ref response);
            
            Log.Information(response.Text);
            Assert.AreEqual("Permission Denied",response.Text);
        }

        [Test]
        public void DispatchSuccessTest()
        {
            var dbMock = new Mock<ConfigurationDbContext>();
            var service = new Mock<PermissionService>(dbMock.Object);
            service
                .Setup(permissionService =>
                    permissionService.CheckPermission(It.IsAny<BotUser>(), It.IsAny<string>()))
                .Returns(true);
            var permissionInterceptor = new Mock<PermissionInterceptor>(service.Object);
            var dispatcher = new MessageDispatcher(new []{permissionInterceptor.Object});
            var request = new BotMsgRequest {Content = "/help 111"
                ,From  = new BotUser{Id = 1,Type = 1,UserName = "test",UserId = "test"}
                ,MessageSource = MessageSource.Telegram
                ,MsgId = "msgId",
                Time = DateTime.Now
            };
            var response = new BotMsgResponse();

            dispatcher.DoDispatch(ref request,ref response);
            Assert.AreNotEqual("Permission Denied",response.Text);
            Assert.AreNotEqual("Command Not Found",response.Text);
            Log.Information(response.Text);
        }
        
    }
}