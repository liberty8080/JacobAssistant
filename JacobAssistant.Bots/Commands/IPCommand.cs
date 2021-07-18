using JacobAssistant.Services;
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

        public IResult Execute(object sender, MessageEventArgs e)
        {
            return new Result {Text = IpService.GetPublicIp()};
        }
    }
}