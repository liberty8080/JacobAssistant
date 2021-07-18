using System;
using JacobAssistant.Bots.Commands;
using JacobAssistant.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace JacobAssistant.Bots.TelegramBots
{
    public class CommandHandler : BaseMessageHandler
    {
 
        public override IResult Handle(MessageEventArgs e)
        {
            throw new  NotImplementedException();
        }

        public override IResult Handle(object sender, MessageEventArgs e)
        {
            var msg = e.Message.Text;

            if (!IsCommand(msg))
            {
                Next.Handle(e);
                return null;
            }

            Log.Information($"Received Command: \"{msg}\" from user: {e.Message?.Chat?.Username}({e.Message?.Chat?.Id})");
            var cmdStr = msg.Substring(1);
            Log.Debug($"CmdStr: {cmdStr}");
            var command = ICommand.GetCommand(cmdStr);
            Log.Debug($"cmdType: {command}");
            var result = command.Execute(null, e);
            /*var client = (TelegramBotClient) sender;
            client.SendTextMessageAsync(long.Parse(_configuration[ConfigMapping.TelegramAdminId])
                ,result.Text);*/
            return result;
        }

        private static bool IsCommand(string msg)
        {
            return msg.StartsWith("/");
        }
    }
}