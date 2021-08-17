using System;
using Microsoft.AspNetCore.Mvc;

namespace JacobAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WechatController
    {
        [HttpGet]
        public string Verify()
        {
            //todo:WebHook加解密
            throw new NotImplementedException();
        }
    }
}