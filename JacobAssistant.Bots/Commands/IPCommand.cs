using JacobAssistant.Bots.Messages;
using JacobAssistant.Services;
using JacobAssistant.Services.Interfaces;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Commands
{
    public class IPCommand : ICommand
    {
        public IPCommand()
        {
            Name = "ip";
            Desc = "返回公网ip地址";
        }

        public string Name { get; set; }
        public string Desc { get; set; }
        public int Order { get; set; }

        public IResult Execute<T>(T sender, MsgEventArgs e) where T:IAnnounceService
        {
            return new Result {Text = IpService.GetPublicIp()};
        }
    }
}