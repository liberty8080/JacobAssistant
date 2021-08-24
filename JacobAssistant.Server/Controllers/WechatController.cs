using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Common.Models;
using Microsoft.AspNetCore.Mvc;
using JacobAssistant.Services.Wechat;

namespace JacobAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WechatController : ControllerBase
    {
        private readonly WechatCryptographyService _cryptographyService;
        private readonly IMessageDispatcher _dispatcher;

        public WechatController(WechatCryptographyService cryptographyService, IMessageDispatcher dispatcher)
        {
            _cryptographyService = cryptographyService;
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public string VerifyUrl(string msg_signature, string timestamp, string nonce, string echostr)
        {
            return _cryptographyService.VerifyUrl(msg_signature, timestamp, nonce, echostr);
        }

        [HttpPost]
        public async Task<string> Callback()
        {
            var msgSignature = Request.Query["msg_signature"];
            var timestamp = Request.Query["timestamp"];
            var nonce = Request.Query["nonce"];
            var msg = await DecryptMsg(msgSignature, timestamp, nonce);
            
            DispatchMsg(ref msg);
            
            
            var result = _cryptographyService.Encrypt(msg, timestamp, nonce);
            return result;
        }

        private async Task<string> DecryptMsg(string msgSignature, string timestamp, string nonce)
        {
            string content;
            using (var reader = new StreamReader(Request.Body))
            {
                content = await reader.ReadToEndAsync();
            }

            var msg = _cryptographyService.Decrypt(msgSignature, timestamp, nonce, content);
            return msg;
        }

        private void DispatchMsg(ref string msg)
        {
            /*var doc = new XmlDocument();
            doc.LoadXml(msg);
            var request = new BotMsgRequest();
            var msgType = "";
            try
            {
                //process request
                var from = doc.DocumentElement["FromUserName"].InnerText;
                msgType = doc.DocumentElement["MsgType"].InnerText;
                request.MessageSource = MessageSource.Wechat;
                request.Content = doc.DocumentElement["Content"].InnerText;
                request.Time = DateTime.Now;
                request.MsgId = doc.DocumentElement["MsgId"].InnerText;
                request.From = new BotUser {UserName = from, UserId = from, Type = 1};
            }
            catch (Exception)
            {
                throw new ApplicationException("解析微信消息失败！");
            }*/
            var request = BotMsgRequest.ParseFromWechat(msg);
            var response = new BotMsgResponse
            {
                Source = MessageSource.Wechat,ContentType = ResponseContentType.PlainText,
                To = request.From
            };
            
            if (request.ContentType == RequestContentType.Text)
            {
                _dispatcher.DoDispatch(ref request, ref response);
                /*var doc = new XmlDocument();
                doc.LoadXml(msg);
                doc.DocumentElement["Content"].InnerText = response.Text;
                msg = doc.InnerXml;*/
                ApplyContentChange(ref msg,response.Text);
            }
        }

        private void ApplyContentChange(ref string msg,string content)
        {
            var doc = new XmlDocument();
            doc.LoadXml(msg);
            doc.DocumentElement["Content"].InnerText = content;
            msg = doc.InnerXml;
        }
    }
}