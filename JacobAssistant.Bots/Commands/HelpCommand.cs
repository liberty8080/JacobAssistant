using System;
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

        public IResult Execute(object sender, MessageEventArgs e)
        {
            string result = null;
            Log.Debug($"Help Executed by {e.Message?.Chat?.Username}");
            var commands = ICommand.GetCommands();
            foreach (var command in commands)
            {
                var cmd = (ICommand) Activator.CreateInstance(command);
                if (cmd == null)
                {
                    throw new ApplicationException("Help命令执行失败");
                }

                result += $"/{cmd.Name} {cmd.Desc}\n";
            }

            return new Result {Text = result};
        }
    }
}