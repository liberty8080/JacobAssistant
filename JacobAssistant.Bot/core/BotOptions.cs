using Telegram.Bot.Types;

namespace JacobAssistant.Bot
{
    public class BotOptions
    {
        public string Token { get; }
        public ChatId AdminId { get; }
        public ChatId AnnounceChannel { get; }

        public BotOptions(string token,ChatId adminId,ChatId announceChannel)
        {
            Token = token;
            AdminId = adminId;
            AnnounceChannel = announceChannel;
        }
        
        
    }
}