using System.Linq;
using JacobAssistant.Bots.Commands;
using JacobAssistant.Bots.TelegramBots;
using Serilog;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Messages
{
    public class CommandHandler : BaseMessageHandler
    {
        public override IResult Handle(MessageEventArgs e)
        {
            return Handle(this, e);
        }

        public override IResult Handle(object sender, MessageEventArgs e)
        {
            var msg = e.Message.Text;

            if (!IsCommand(msg[1..]))
            {
                Next.Handle(e);
                return null;
            }

            Log.Information(
                $"Received Command: \"{msg}\" from user: {e.Message?.Chat?.Username}({e.Message?.Chat?.Id})");
            var cmdStr = msg[1..];
            Log.Debug($"CmdStr: {cmdStr}");
            var command = ICommand.GetCommand(cmdStr);
            Log.Debug($"cmdType: {command}");
            var result = command.Execute(sender, e);
            return result;
        }

        private static bool IsCommand(string msg)
        {
            return ICommand.GetCommands().ToList().Exists(item=>item.Name==msg);
        }
    }
}