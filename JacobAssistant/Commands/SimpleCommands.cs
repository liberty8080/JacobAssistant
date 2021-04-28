using System;
using System.Linq;
using JacobAssistant.Bot;
using Telegram.Bot.Args;

namespace JacobAssistant.Commands
{
    [TextCommand]
    public class SimpleCommands
    {

  
        [Cmd("help")]
        public void Help(MessageEventArgs e,params string[] args)
        {
            Console.WriteLine(args.Length);
            foreach (var s in args)
            {
                Console.WriteLine(s);
            }
        }
        
    }
}