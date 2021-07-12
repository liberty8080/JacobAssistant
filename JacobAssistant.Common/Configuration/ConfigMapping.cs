using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Models;

namespace JacobAssistant.Common.Configuration
{
    public static class ConfigMapping
    {
        public const string TelegramDevBotToken = "Telegram_DevBotToken";
        public const string TelegramProdBotToken = "Telegram_ProdBotToken";
        public const string TelegramAnnounceChannelId = "Telegram_AnnounceChannelId";
        public const string TelegramLogChannelId = "Telegram_LogChannelId";
        public const string TelegramAdminId = "Telegram_Admin_Id";

        //todo: v2做活,(通用sspannel爬虫,v2 trojan ssr解析生成clash订阅),过期时间,流量查询
        public const string V2SubName = "v2sub";

        public const string WechatAnnounceChannelId = "Wechat_AnnounceChannelId";
        public const string WechatCorpId = "Wechat_CorpId";
        public const string WechatCorpSecret = "Wechat_CorpSecret";


        public static List<string> AllConfigKeys()
        {
            var info = typeof(ConfigMapping);
            var fields = info.GetFields();

            return (from f in fields where f.FieldType == typeof(string) select f.GetValue(null) as string).ToList();
        }
    }
}