namespace JacobAssistant.Bots.Messages
{
    public class MsgEventArgs
    {
        public BotMessage Message { get; set; }
        public MessageSource Telegram { get; set; }
    }

    public enum MessageSource
    {
        Telegram,Wechat
    }
}