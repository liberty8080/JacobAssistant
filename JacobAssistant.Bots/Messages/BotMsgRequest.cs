using System;
using JacobAssistant.Common.Models;
using Telegram.Bot.Types;
using static JacobAssistant.Bots.Commands.ICommand;

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
                var parsed = TryParseCommandName(Content, out var commandName);
                return parsed ? commandName : null;
            }
        }

        public BotMsgRequest()
        {
        }

        public BotMsgRequest(string msgId, string content, BotUser @from, DateTime time, MessageSource messageSource)
        {
            MsgId = msgId;
            Content = content;
            From = @from;
            Time = time;
            MessageSource = messageSource;
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