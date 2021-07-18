using JacobAssistant.Bots.Commands;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.TelegramBots
{
    public abstract class BaseMessageHandler:IMessageHandler
    {

        protected IMessageHandler Next;
        
        public void SetNext(IMessageHandler handler)
        {
            Next = handler;
        }

        public abstract IResult Handle(MessageEventArgs e);
        public abstract IResult Handle(object sender, MessageEventArgs e);
    }
}