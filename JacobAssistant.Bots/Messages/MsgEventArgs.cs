namespace JacobAssistant.Bots.Messages
{
    public class MsgEventArgs
    {
        public BotMsgRequest MsgRequest { get; set; }
    }

    public enum MessageSource
    {
        Telegram,Wechat
    }
}