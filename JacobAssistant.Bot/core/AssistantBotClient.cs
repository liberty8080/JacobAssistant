using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace JacobAssistant.Bot.core
{
    public class AssistantBotClient
    {
        private BotOptions Options { get; set; }
        private TelegramBotClient Client { get; set; }

        public AssistantBotClient(BotOptions options)
        {
            Options = options;
        }

        public void Start()
        {
            Client = new TelegramBotClient(Options.Token);
            Client.OnMessage += OnMessage;
            Client.StartReceiving();
            Client.SendTextMessageAsync(Options.AdminId,"重新上线");
        }

        public void ReloadOptions(BotOptions options)
        {
            Client.StopReceiving();
            Options = options;
            Start();
        }

        private async void OnMessage(object sender,MessageEventArgs e)
        {
            await Client.SendTextMessageAsync(Options.AdminId, Options.AdminId);
        }
        
        
    }
}