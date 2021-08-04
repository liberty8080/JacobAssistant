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

namespace JacobAssistant.Bots.TgBots
{
    public class AssistantBotClient : IAnnounceService
    {
        private readonly PermissionHandler _permissionHandler;

        public AssistantBotClient(BotOptions options,
            PermissionHandler permissionHandler)
        {
            _permissionHandler = permissionHandler;
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
            
            IMessageHandler command = new CommandHandler();
            _permissionHandler.SetNext(command);
            
            var eventArgs = new MsgEventArgs
            {
                Message = new BotMessage(e.Message)
            };
            try
            {
                var result = _permissionHandler.Handle(this, eventArgs);
                ReplyMessage(e, result.Text);
            }
            catch (Exception ex)
            {
                Log.Error($"command execute failed!\n{ex.StackTrace}");
                ReplyMessage(e, ex.Message);
            }
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