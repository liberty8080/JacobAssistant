using System;
using Telegram.Bot.Types;

namespace JacobAssistant.Bots.Messages
{
    public class BotMessage
    {
        public string Content { get; set; }
        public string From { get; set; }
        public DateTimeOffset Time { get; set; }

        public BotMessage(Message message)
        {
            Content = message.Text;
            // From = message.From.;
        }
    }
}