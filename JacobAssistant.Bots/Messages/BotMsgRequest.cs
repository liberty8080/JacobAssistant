using System;
using System.Diagnostics;
using System.Xml;
using JacobAssistant.Common.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static JacobAssistant.Bots.Commands.ICommand;

namespace JacobAssistant.Bots.Messages
{
    public class BotMsgRequest
    {
        public string MsgId { get; set; }
        public string Content { get; set; }
        public BotUser From { get; set; }
        public DateTime Time { get; set; }
        public MessageSource MessageSource { get; set; }

        public RequestContentType ContentType { get; set; }

        public string Command
        {
            get
            {
                var parsed = TryParseCommandName(Content, out var commandName);
                return parsed ? commandName : null;
            }
        }

        public BotMsgRequest()
        {
        }


        public BotMsgRequest(Message message)
        {
            Content = message.Text;
            From = CastUser(message.From);
            MessageSource = MessageSource.Telegram;
            Time = message.Date;
            MsgId = message.MessageId.ToString();
            ContentType = message.Type switch
            {
                MessageType.Voice => RequestContentType.Voice,
                MessageType.Text => RequestContentType.Text,
                MessageType.Video => RequestContentType.Video,
                MessageType.Location => RequestContentType.Location,
                MessageType.Photo => RequestContentType.Image,
                _ => RequestContentType.Other
            };
        }

        public static BotMsgRequest ParseFromWechat(string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                var request = new BotMsgRequest();
                //process request
                var from = doc.DocumentElement["FromUserName"].InnerText;

                request.ContentType = RequestContentType.Text;
                request.MessageSource = MessageSource.Wechat;
                request.Content = doc.DocumentElement["Content"].InnerText;
                request.Time = DateTime.Now;
                request.MsgId = doc.DocumentElement["MsgId"].InnerText;
                request.From = new BotUser {UserName = from, UserId = from, Type = UserType.Wechat};

                var msgType = doc.DocumentElement["MsgType"].InnerText;
                request.ContentType = msgType switch
                {
                    "text" => RequestContentType.Text,
                    "image" => RequestContentType.Image,
                    "voice" => RequestContentType.Voice,
                    "video" => RequestContentType.Video,
                    "location" => RequestContentType.Location,
                    "link" => RequestContentType.Link,
                    _ => RequestContentType.Other
                };
                return request;
            }
            catch (Exception)
            {
                throw new ApplicationException("解析微信消息失败！");
            }
        }

        private static BotUser CastUser(User tgUser)
        {
            return new()
            {
                UserId = tgUser.Id.ToString(),
                UserName = tgUser.FirstName,
                Type = UserType.Telegram
            };
        }
    }

    public enum RequestContentType
    {
        Text,
        Image,
        Voice,
        Video,
        Location,
        Link,
        Other
    }
}