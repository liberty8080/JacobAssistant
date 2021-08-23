using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using JacobAssistant.Common.Util;
using JacobAssistant.Services.Wechat;
using Microsoft.AspNetCore.Http;

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

        [HttpGet]
        public string VerifyUrl(string msg_signature, int timestamp, string nonce, string echostr)
        {
            return _cryptographyService.VerifyUrl(msg_signature, timestamp, nonce, echostr);
        }

        [HttpPost]
        public string Callback(HttpRequestMessage request)
        {
            var content = request.Content?.ReadAsStringAsync().Result;
            Debug.Assert(request.RequestUri != null, "request.RequestUri != null");
            var query = System.Web.HttpUtility.ParseQueryString(request.RequestUri.Query);
            var timestamp = int.Parse(query["timestamp"] ?? string.Empty);
            var result = _cryptographyService.Decrypt(query["msg_signature"],timestamp, query["nonce"], content);
            return result;
        }
    }

    public class WechatMsg
    {
        public string ToUserName { get; set; }
        public string AgentID { get; set; }
        public string Encrypt { get; set; }
    }
}