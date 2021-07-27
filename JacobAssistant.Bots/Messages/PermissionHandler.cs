using System;
using JacobAssistant.Bots.Commands;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Messages
{
    public class PermissionHandler:BaseMessageHandler
    {

        public PermissionHandler()
        {
            //todo: inject permissionCheck service
        }
        public override IResult Handle(MsgEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override IResult Handle<T>(T sender, MsgEventArgs e)
        {
            switch (e.Message.MessageSource)
            {
                case MessageSource.Telegram:
                    
                    break;
                case MessageSource.Wechat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}