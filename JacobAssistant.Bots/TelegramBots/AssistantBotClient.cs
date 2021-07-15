using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using JacobAssistant.Bots.Exceptions;
// using log4net;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace JacobAssistant.Bots.TelegramBots
{
    public class AssistantBotClient
    {
        // private readonly ILog _log = LogManager.GetLogger(typeof(AssistantBotClient));
        private readonly IServiceProvider _provider;
//todo: 用新api替换
        public AssistantBotClient(BotOptions options, IServiceProvider provider)
        {
            _provider = provider;
            Options = options;
        }

        private BotOptions Options { get; set; }
        private TelegramBotClient Client { get; set; }

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

        public async void ReplyMessage(MessageEventArgs e, string text)
        {
            await SendMessage(e.Message.Chat.Id, text);
        }

        public async Task<Message> SendMessageToJacob(string text)
        {
            return await SendMessage(Options.AdminId, text);
        }

        public async void SendMessageToChannel(string text)
        {
            await SendMessage(Options.AnnounceChannel, text);
        }

        public async Task<Message> SendMessage(ChatId chatId, string text)
        {
            return await Client.SendTextMessageAsync(chatId, text);
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
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
            }
        }


        private static BotPermission CommandPermission(MethodInfo method)
        {
            // ReSharper disable once PossibleNullReferenceException
            return method.GetCustomAttribute<Cmd>().Permission;
        }

        private bool IsAdmin(MessageEventArgs e)
        {
            return e.Message.From.Id == Options.AdminId.Identifier;
        }

        public static MethodInfo MatchCommand(string message)
        {
            var method = from m in GetCommands()
                where m.GetCustomAttribute<Cmd>()?.Name == message
                select m;
            var methodInfos = method.ToList();
            if (methodInfos.Count > 0)
                return methodInfos.First();

            throw new CommandNotFoundExceptions();
        }

        public static IEnumerable<MethodInfo> GetCommands()
        {
            var methods = from cls in Assembly.GetExecutingAssembly().GetTypes()
                where cls.IsClass && cls.GetCustomAttribute<TextCommandAttribute>() != null
                from m in cls.GetMethods()
                where m.GetCustomAttribute<Cmd>() != null
                select m;
            return methods;
        }
    }
}