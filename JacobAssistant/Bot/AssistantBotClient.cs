using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace JacobAssistant.Bot
{
    public class AssistantBotClient
    {
        private readonly IServiceProvider _provider;
        private BotOptions Options { get; set; }
        private TelegramBotClient Client { get; set; }

        public AssistantBotClient(BotOptions options,IServiceProvider provider)
        {
            _provider = provider;
            Options = options;
        }

        public void Start()
        {
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

        public async void ReplyMessage(MessageEventArgs e, string text) =>
            await SendMessage(e.Message.Chat.Id, text);

        public async Task<Message> SendMessageToJacob(string text) => await SendMessage(Options.AdminId, text);

        public async void SendMessageToChannel(string text) => await SendMessage(Options.AnnounceChannel, text);

        public async Task<Message> SendMessage(ChatId chatId, string text) =>
            await Client.SendTextMessageAsync(chatId, text);

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text.StartsWith("/"))
            {
                var s = e.Message.Text.Split(" ");
                var m = MatchCommand(s[0].Replace("/", "")).ToList();
                var method = m.First();
                var obj = _provider.GetService(method.ReflectedType ?? throw new InvalidOperationException());
                Debug.Assert(obj != null, "命令解析错误！");
                method.Invoke(obj, new object[]{e,s[1..]});
            }
            else
            {
                ReplyMessage(e, "听不懂,给👴爬");
            }
        }

        public static IEnumerable<MethodInfo> MatchCommand(string message)
        {
            var method = from cls in Assembly.GetExecutingAssembly().GetTypes()
                where cls.IsClass && cls.GetCustomAttribute<TextCommandAttribute>() != null
                from m in cls.GetMethods()
                where m.GetCustomAttribute<Cmd>() != null && m.GetCustomAttribute<Cmd>()?.Name == message
                select m;
            return method;
        }
    }
}