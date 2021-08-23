using System;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Common.Util;
using Microsoft.Extensions.Options;
using Serilog;

namespace JacobAssistant.Services.Wechat
{
    public class WechatCryptographyService
    {
        private readonly AppOptions _options;

        public WechatCryptographyService(IOptions<AppOptions> options)
        {
            _options = options.Value;
        }

        public string VerifyUrl(string msgSignature, int timestamp, string nonce, string echostr)
        {
            var msgCrypt =
                new WXBizMsgCrypt(_options.WechatAppToken, _options.WechatAppAESKey, _options.WechatCorpId);
            var result = "";
            msgCrypt.VerifyURL(msgSignature, timestamp.ToString(), nonce, echostr, ref result);
            return result;
        }

        public string Decrypt(string sMsgSignature, int sTimeStamp, string sNonce, string sPostData)
        {
            var msgCrypt = new WXBizMsgCrypt(_options.WechatAppToken, _options.WechatAppAESKey, _options.WechatCorpId);
            var rMsg = "";
            var code =msgCrypt.DecryptMsg(sMsgSignature,sTimeStamp.ToString(),sNonce,sPostData,ref rMsg);
            if (code != 0)
            {
                throw new ApplicationException($"wechatMsg Decrypt Failed! Error Code: {code}");
            }
            return rMsg;
        }
    }
}