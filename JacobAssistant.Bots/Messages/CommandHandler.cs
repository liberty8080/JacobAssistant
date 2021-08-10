using System.Linq;
using JacobAssistant.Bots.Commands;
using JacobAssistant.Services.Interfaces;
using Serilog;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Messages
{
    public class CommandHandler : BaseMessageHandler
    {
        private static bool IsCommand(string msg)
        {
            return ICommand.GetCommands().ToList().Exists(item=>item.Name==msg);
        }

        public override IResult Handle(MsgEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public override IResult Handle<T>(T sender, MsgEventArgs e)
        {
            var msg = e.MsgRequest.Content;

            if (!ICommand.IsCommand(msg[1..]))
            {
                Next.Handle(e);
                return null;
            }

            Log.Information(
                $"Received Command: \"{msg}\" from user: {e.MsgRequest.From.UserName}({e.MsgRequest.From.UserId})");
            var cmdStr = msg[1..];
            Log.Debug($"CmdStr: {cmdStr}");
            var command = ICommand.GetCommand(cmdStr);
            Log.Debug($"cmdType: {command}");
            var result = command.Execute(sender, e);
            return result;
        }
    }
}