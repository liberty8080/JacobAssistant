using System;
using Telegram.Bot.Types;

namespace JacobAssistant.Bots.Messages
{
    public class BotMessage
    {
        public string MsgId { get; set; }
        public string Content { get; set; }
        public BotUser From { get; set; }
        public DateTime Time { get; set; }
        public MessageSource MessageSource { get; set; }

        public BotMessage(Message message)
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
                UserName = tgUser.FirstName
            };
        }
    }
}