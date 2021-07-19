using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;

namespace JacobAssistant.Bots.Commands
{
    public interface ICommand
    {
        string Name { get; set; }
        string Desc { get; set; }
        int Order { get; set; }
        IResult Execute(object sender, MessageEventArgs e);

        static IEnumerable<Type> GetCommands()
        {
            var x = Assembly.GetAssembly(typeof(ICommand))?.GetTypes()
                .Where(item => typeof(ICommand).IsAssignableFrom(item)).Where(item => item.IsClass);
            return x;
        }

        static ICommand GetCommand(string name)
        {
            var cmd = GetCommands()
                .FirstOrDefault(item => item.Name.Replace("Command", "").ToUpper().Equals(name.ToUpper()));
            return cmd != null ? (ICommand) Activator.CreateInstance(cmd) : Activator.CreateInstance<HelpCommand>();
        }

        static bool IsCommand(string name)
        {
            throw new NotImplementedException();
        }
    }
}