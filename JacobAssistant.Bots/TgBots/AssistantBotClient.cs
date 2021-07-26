using System;
using System.Threading.Tasks;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Services.Interfaces;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace JacobAssistant.Bots.TelegramBots
{
    public class AssistantBotClient:IAnnounceService
    {

        public AssistantBotClient(BotOptions options)
        {
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
            /*try
            {
                Log.Information($"{e.Message}");
                // not text type
                if (!e.Message.Type.Equals(MessageType.Text))
                {
                    var options = new JsonSerializerOptions {WriteIndented = true};
                    SendMessageToChannel(JsonSerializer.Serialize(e.Message, options));
                    return;
                }

                // not command
                if (!e.Message.Text.StartsWith("/")) return;

                // command invoke
                var s = e.Message.Text.Split(" ");
                var method = MatchCommand(s[0].Replace("/", ""));
                var obj = _provider.GetService(method.ReflectedType ?? throw new InvalidOperationException());
                Debug.Assert(obj != null, "命令解析错误！");
                // admin only
                if (CommandPermission(method).Equals(BotPermission.Admin) && !IsAdmin(e))
                {
                    ReplyMessage(e, "听不懂,给👴爬");
                }
                else
                {
                    var result = (string) method.Invoke(obj, new object[] {e, s[1..]});
                    ReplyMessage(e, result);
                }
            }
            catch (CommandNotFoundExceptions)
            {
                if (IsAdmin(e)) ReplyMessage(e, "command not found");
            }
            catch (Exception exception)
            {
                // _log.Error("Onmessage error", exception);
                var options = new JsonSerializerOptions {WriteIndented = true};
                SendMessageToChannel(exception.ToString());
                SendMessageToChannel(JsonSerializer.Serialize(e.Message, options));
            }*/
            //todo:permissionHandler
            IMessageHandler command = new CommandHandler();
            try
            {
                var result = command.Handle(sender, e);
                ReplyMessage(e, result.Text);
            }
            catch (Exception ex)
            {
                ReplyMessage(e, ex.Message);
            }
        }


        public void Announce(string message)
        {
            SendMessageToChannel(message);
        }
    }
}