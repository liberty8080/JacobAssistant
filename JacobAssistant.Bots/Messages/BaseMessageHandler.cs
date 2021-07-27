using JacobAssistant.Bots.Commands;
using JacobAssistant.Services.Interfaces;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Messages
{
    public abstract class BaseMessageHandler : IMessageHandler
    {
        protected IMessageHandler Next;

        public void SetNext(IMessageHandler handler)
        {
            Next = handler;
        }

        public abstract IResult Handle(MsgEventArgs e);
        public abstract IResult Handle<T>(T sender, MsgEventArgs e) where T:IAnnounceService;
    }
}