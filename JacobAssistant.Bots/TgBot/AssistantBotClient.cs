using System;
using System.Threading;
using System.Threading.Tasks;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Common.Models;
using JacobAssistant.Services.Interfaces;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Extensions.Polling;

namespace JacobAssistant.Bots.TgBot
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
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            IUpdateHandler handler = new DefaultUpdateHandler(HandleUpdateAsync,HandleErrorAsync);
            Client.StartReceiving(handler,cancellationToken);
            Client.SendTextMessageAsync(Options.AdminId, "重新上线", cancellationToken: cancellationToken);

        }
        

        public async void ReplyMessage(Message message, string text)
        {
            await SendMessage(message.Chat.Id, text);
        }

        private async Task SendMessageToChannel(string text)
        {
            await SendMessage(Options.AnnounceChannel, text);
        }

        private async Task SendMessage(ChatId chatId, string text)
        {
            await Client.SendTextMessageAsync(chatId, text);
        }
        
        public async void Announce(string message)
        {
           await SendMessageToChannel(message);
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
                Log.Error(e,"user id cast failed！");
                throw;
            }
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message )
            {
                var message = update.Message;
                // only receive text message
                if (message.Type != MessageType.Text) return;
                var request = new BotMsgRequest(message);
            
                var response = new BotMsgResponse
                {
                    ContentType = ResponseContentType.PlainText,
                    Source = MessageSource.Telegram,
                    To = request.From
                };
                _dispatcher.DoDispatch(ref request,ref response);
            
               await SendMessage(long.Parse(response.To.UserId),response.Text);
            }
        }

        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            await SendMessageToChannel(exception.Message);
        }
    }
}