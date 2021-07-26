using Telegram.Bot.Types;

namespace JacobAssistant.Bots.TelegramBots
{
    public class BotOptions
    {
        public BotOptions(string token, ChatId adminId, ChatId announceChannel)
        {
            Token = token;
            AdminId = adminId;
            AnnounceChannel = announceChannel;
        }

        public string Token { get; }
        public ChatId AdminId { get; }
        public ChatId AnnounceChannel { get; }
    }
}