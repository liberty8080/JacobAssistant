using JacobAssistant.Bots.Commands;
using JacobAssistant.Bots.Messages;
using NUnit.Framework;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace JacobAssistant.Tests.Bots
{
    [TestFixture]
    public class MessageHandlersTest : BaseTest
    {
        public MessageEventArgs GetMessage(string text)
        {
            return new MessageEventArgs(new Message {Text = "/help -h", Chat = new Chat {Username = "Test"}});
        }

        [Test]
        public void CommandHandlerTest()
        {
            IMessageHandler handler = new CommandHandler();
            handler.Handle(null, GetMessage("/help"));
        }

        [Test]
        public void HelpCommandTest()
        {
            HelpCommand command = new();
            var result = command.Execute(null, GetMessage("/help"));
            Assert.NotNull(result.Text);
        }

        [Test]
        public void IpServiceTest()
        {
            IPCommand command = new();
            var result = command.Execute(null, GetMessage("/ip"));
            Assert.That(result.Text,
                Does.Match(
                    @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$"));
        }
    }
}