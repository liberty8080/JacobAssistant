using JacobAssistant.Common.Models;

namespace JacobAssistant.Bots.Messages
{
    public class BotMsgResponse
    {
        public string Text { get; set; }
        public BotUser To { get; set; }
        public ResponseContentType ContentType { get; set; }
        public MessageSource Source { get; set; }
    }

    public enum ResponseContentType
    {
        PlainText,Markdown,ImageAndMsg
    }
}