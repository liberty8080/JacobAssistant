using System;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Services;
using JacobAssistant.Services.Interfaces;

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



        public void Execute(ref BotMsgRequest request, ref BotMsgResponse response)
        {
            try
            {
                var ip = IpService.GetPublicIp();
                if (string.IsNullOrEmpty(ip))
                {
                    response.Text = "Can't Get Your IP";
                    return;
                }
                response.Text = ip;
            }
            catch (Exception)
            {
                response.Text = "Execute Failed";
            }
            
        }
    }
}