using System;
using JacobAssistant.Bots.Commands;
using JacobAssistant.Common.Models;
using Telegram.Bot.Types;

namespace JacobAssistant.Bots.Messages
{
    public class BotMsgRequest
    {
        public string MsgId { get; set; }
        public string Content { get; set; }
        public BotUser From { get; set; }
        public DateTime Time { get; set; }
        public MessageSource MessageSource { get; set; }

        public string Command
        {
            get
            {
                ICommand.TryParseCommandName(Content, out var commandName);
                return commandName;
            }
        }

        public BotMsgRequest(Message message)
        {
            Content = message.Text;
            From = CastUser(message.From);
            MessageSource = MessageSource.Telegram;
            Time = message.Date;
            MsgId = message.MessageId.ToString();
        }

        public static BotUser CastUser(User tgUser)
        {
            return new()
            {
                UserId = tgUser.Id.ToString(),
                UserName = tgUser.FirstName,
                Type = UserType.Telegram
            };
        }
        
        
    }
}