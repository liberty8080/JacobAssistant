using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Services.Interfaces;

namespace JacobAssistant.Bots.Commands
{
    public interface ICommand
    {
        string Name { get; set; }
        string Desc { get; set; }
        int Order { get; set; }
        IResult Execute<T>(T sender, MsgEventArgs e) where T:IAnnounceService;

        void Execute(ref BotMsgRequest request,ref BotMsgResponse response);

        static IEnumerable<ICommand> GetCommands()
        {
            var x = Assembly.GetAssembly(typeof(ICommand))?.GetTypes()
                .Where(item => typeof(ICommand).IsAssignableFrom(item)).Where(item => item.IsClass)
                .Select(item=>(ICommand)Activator.CreateInstance(item))
                // 不设顺序的默认往后排
                .OrderBy(c=> c?.Order ?? 10);
            return x;
        }

        /// <summary>
        /// Get ICommand Instance
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        static bool HasCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                return false;
            }
            return GetCommand(commandName) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool IsCommand(string content)
        {
            return TryParseCommandName(content,out var commandName);
        }
        
        static string ParseCommandName(BotMsgRequest msgRequest)
        {
            var msg = msgRequest.Content;
            return TryParseCommandName(msg, out var commandName) ? commandName : null;
        }

        static bool TryParseCommandName(string content,out string commandName)
        {
            var isCommand = Regex.IsMatch(content,@"/[a-z]( [a-z])*");
            
            if (isCommand)
            {
                commandName = content.Split(" ")[0].Replace("/" ,"");
                return true;
            }

            commandName = null;
            return false;
        }
        
    }
}