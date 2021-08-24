using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JacobAssistant.Services.Wechat;

namespace JacobAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WechatController:ControllerBase
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
        public async Task<string> Callback()
        {
            var msgSignature = Request.Query["msg_signature"];
            var timestamp = Request.Query["timestamp"];
            var nonce = Request.Query["nonce"];
            string content;
            using (var reader = new StreamReader(Request.Body))
            {
                content=await reader.ReadToEndAsync();
            }
            var result = _cryptographyService.Decrypt(msgSignature, timestamp, nonce, content);
            return result;
        }
    }

}