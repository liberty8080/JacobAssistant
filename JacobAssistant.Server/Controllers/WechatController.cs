using System;
using Microsoft.AspNetCore.Mvc;
using JacobAssistant.Common.Util;
using JacobAssistant.Services.Wechat;

namespace JacobAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WechatController
    {
        private readonly WechatCryptographyService _cryptographyService;

        public WechatController(WechatCryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }
        [HttpGet("wechat_verify")]
        public string CallBack(string msg_signature,int timestamp ,string nonce,string echostr)
        {
          return  _cryptographyService.VerifyUrl(msg_signature, timestamp, nonce, echostr);
        }
        
        
    }
}