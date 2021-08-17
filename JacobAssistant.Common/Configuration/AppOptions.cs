namespace JacobAssistant.Common.Configuration
{
    /// <summary>
    /// 各种配置
    /// </summary>
    public class AppOptions
    {
        public const string App = "App";

        public string TelegramDevBotToken { get; set; }
        public string TelegramProdBotToken { get; set; }
        public long TelegramAnnounceChannelId { get; set; }
        public long TelegramLogChannelId { get; set; }
        public long TelegramAdminId { get; set; }

        public string WechatAnnounceChannelId { get; set; }
        public string WechatCorpId { get; set; }
        public string WechatAppSecret { get; set; }
        public string WechatAppId { get; set; }
        public string WechatAppToken { get; set; }
        public string WechatAppAESKey { get; set; }
    }
}