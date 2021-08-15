using System;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Services.Interfaces;
using Serilog;

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
        

        public void Execute(ref BotMsgRequest request, ref BotMsgResponse response)
        {
            string result = null;
            try
            {
                Log.Information($"Help Command Executed By {request.From.UserName ?? request.From.UserId}");

                var commands = ICommand.GetCommands();
                foreach (var command in commands)
                {
                    result += $"/{command.Name} {command.Desc}\n";
                }

                response.Text = result;
            }
            catch (Exception)
            {
                response.Text = "Command Executed Failed";
            }
            
        }
    }
}