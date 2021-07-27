using JacobAssistant.Bots.Commands;
using JacobAssistant.Services.Interfaces;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Messages
{
    /// <summary>
    /// Execute Command By TextMessage
    /// </summary>
    public interface IMessageHandler
    {
        void SetNext(IMessageHandler handler);
         IResult Handle(MsgEventArgs e);
         IResult Handle<T>(T sender, MsgEventArgs e) where T:IAnnounceService;
    }
}