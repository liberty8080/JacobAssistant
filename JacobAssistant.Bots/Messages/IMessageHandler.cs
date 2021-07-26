using JacobAssistant.Bots.Commands;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Messages
{
    public interface IMessageHandler
    {
        void SetNext(IMessageHandler handler);
        IResult Handle(MessageEventArgs e);
        IResult Handle(object sender, MessageEventArgs e);
    }
}