using System;
using System.Threading.Tasks;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Common.Models;
using JacobAssistant.Services;
using JacobAssistant.Services.Interfaces;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Ubiety.Dns.Core;

namespace JacobAssistant.Bots.TgBots
{
    public class AssistantBotClient : IAnnounceService
    {
        private readonly IMessageDispatcher _dispatcher;

        public AssistantBotClient(BotOptions options, IMessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            Options = options;
        }

        private BotOptions Options { get; set; }
        private TelegramBotClient Client { get; set; }

        public void Start()
        {
            Log.Information("Bot重新上线");
            Client = new TelegramBotClient(Options.Token);
            Client.OnMessage += OnMessage;
            Client.StartReceiving();
            Client.SendTextMessageAsync(Options.AdminId, "重新上线");
        }

        public void ReloadOptions(BotOptions options)
        {
            Client.StopReceiving();
            Options = options;
            Start();
        }

        public async void ReplyMessage(MessageEventArgs e, string text)
        {
            await SendMessage(e.Message.Chat.Id, text);
        }

        public async void SendMessageToChannel(string text)
        {
            await SendMessage(Options.AnnounceChannel, text);
        }

        public async Task SendMessage(ChatId chatId, string text)
        {
            await Client.SendTextMessageAsync(chatId, text);
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {

            if (e.Message.Type != MessageType.Text) return;
            var request = new BotMsgRequest(e.Message);
            
            var response = new BotMsgResponse
            {
                ContentType = ResponseContentType.PlainText,
                Source = MessageSource.Telegram,
                To = request.From
            };
            _dispatcher.DoDispatch(ref request,ref response);
            
            SendMessage(long.Parse(response.To.UserId),response.Text);

        }


        public void Announce(string message)
        {
            SendMessageToChannel(message);
        }

        public async void SendToUser(string message, BotUser user)
        {
            try
            {
                ChatId chatId = long.Parse(user.UserId);
                await SendMessage(chatId, message);
            }
            catch (Exception e)
            {
                Log.Error("user id cast failed！",e);
                throw;
            }
        }
    }
}