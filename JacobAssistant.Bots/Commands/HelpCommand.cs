using System;
using System.Linq;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Services.Interfaces;
using Serilog;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Commands
{
    public class HelpCommand : ICommand
    {
        public HelpCommand()
        {
            Name = "help";
            Desc = "帮助";
            Order = 0;
        }

        public string Name { get; set; }
        public string Desc { get; set; }
        public int Order { get; set; }

        public IResult Execute<T>(T sender, MsgEventArgs e)where T :IAnnounceService
        {
            string result = null;
            Log.Debug($"Help Executed by {e.Message.From.UserId}");
            var commands = ICommand.GetCommands();
            foreach (var command in commands)
            {
                if (command == null)
                {
                    throw new ApplicationException("Help命令执行失败");
                }

                result += $"/{command.Name} {command.Desc}\n";
            }

            return new Result {Text = result};
        }
    }
}