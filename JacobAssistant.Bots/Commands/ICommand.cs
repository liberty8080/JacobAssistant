using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Services.Interfaces;
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
        IResult Execute<T>(T sender, MsgEventArgs e) where T:IAnnounceService;

        static IEnumerable<ICommand> GetCommands()
        {
            var x = Assembly.GetAssembly(typeof(ICommand))?.GetTypes()
                .Where(item => typeof(ICommand).IsAssignableFrom(item)).Where(item => item.IsClass)
                .Select(item=>(ICommand)Activator.CreateInstance(item))
                // 不设顺序的默认往后排
                .OrderBy(c=> c?.Order ?? 10);
            return x;
        }

        static ICommand GetCommand(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Command Name Can't Be Null!");
            }
            var cmd = GetCommands()
                .FirstOrDefault(item => item.Name.Replace("Command", "").ToUpper().Equals(name.ToUpper()));
            return cmd;
        }

        /// <summary>
        /// Check If Command
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        static bool IsCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                return false;
            }
            return GetCommand(commandName) != null;
        }

        
        static string ParseCommandName(BotMsgRequest msgRequest)
        {
            var msg = msgRequest.Content;
            var commandName = msg.Split(" ")[0].Replace("/" ,"");
            return commandName;
        }
        
    }
}