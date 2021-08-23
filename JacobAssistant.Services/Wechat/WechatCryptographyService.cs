using System;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Common.Util;
using Microsoft.Extensions.Options;

namespace JacobAssistant.Services.Wechat
{
    public class WechatCryptographyService
    {
        private readonly AppOptions _options;

        public WechatCryptographyService(IOptions<AppOptions> options)
        {
            _options = options.Value;
        }

        public string VerifyUrl(string msg_signature,int timestamp ,string nonce,string echostr)
        {
            var msgCrypt =
                new WXBizMsgCrypt(_options.WechatAppToken, _options.WechatAppAESKey, _options.WechatCorpId);
            var result = "";
            msgCrypt.VerifyURL(msg_signature, timestamp.ToString(), nonce,  echostr,ref result);
            return result;
        }
    }
}