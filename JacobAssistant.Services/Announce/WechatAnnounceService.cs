using System;
using System.Net.Http;
using System.Text.Json;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Services.Interfaces;
using JacobAssistant.Services.Wechat;
using Microsoft.Extensions.Options;
using Serilog;

namespace JacobAssistant.Services.Announce
{
    public class WechatAnnounceService : IAnnounceService
    {
        private readonly WechatTokenHolder _tokenHolder;
        private readonly AppOptions _options;
        private const string RequestUrl = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=ACCESS_TOKEN";

        public WechatAnnounceService(WechatTokenHolder tokenHolder,IOptions<AppOptions> options)
        {
            _tokenHolder = tokenHolder;
            _options = options.Value;
        }

        public void Announce(string message)
        {
            
            
        }

        public void SendToUser(string message, BotUser user)
        {
            var msg = new WechatSendMessage(message)
            {
                touser = user?.UserId??_options.WechatAnnounceChannelId, 
                agentid = int.Parse(_options.WechatAppId),
                msgtype = "text"
            };
            var send = JsonSerializer.Serialize(msg);
            HttpContent content = new StringContent(send);
            using var client = new HttpClient();
            try
            {
                var tempRes = client
                    .PostAsync(RequestUrl.Replace("ACCESS_TOKEN", _tokenHolder.Token), content)
                    .Result;
                Log.Information("微信消息已发送");
                var result = tempRes.Content.ReadAsStringAsync().Result;
                Log.Debug(result);
            }
            catch (Exception e)
            {
                throw new WechatAnnounceException("Wechat Announce Failed",e);
            }
        }
    }

    public class WechatSendMessage
    {
        public WechatSendMessage()
        {
            
        }
        public WechatSendMessage(string msg)
        {
            text = new WechatMessageText{content = msg};
        }
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public WechatMessageText text { get; set; }
        public int safe { get; set; }
        public int enable_id_trans { get; set; }
        public int enable_duplicate_check { get; set; }
        public int duplicate_check_interval { get; set; }
    }

    public class WechatMessageText
    {
        public string content { get; set; }
    }

    public class WechatAnnounceException:Exception
    {
        public WechatAnnounceException(string msg,Exception e):base(msg,e)
        {
            
        }
    }
}