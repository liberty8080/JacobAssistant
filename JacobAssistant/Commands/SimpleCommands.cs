using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JacobAssistant.Bot;
using JacobAssistant.Models;
using JacobAssistant.Services;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Args;

namespace JacobAssistant.Commands
{
    [TextCommand]
    public class SimpleCommands
    {
        private readonly IServiceProvider _provider;

        public SimpleCommands(IServiceProvider provider)
        {
            _provider = provider;
        }
        [Cmd("help", Order = 0,Desc = "帮助")]
        public string Help(MessageEventArgs e, params string[] args)
        {
            var commands = AssistantBotClient.GetCommands().ToList()
                .OrderBy(info => info.GetCustomAttribute<Cmd>()?.Order);
            return commands.Select(methodInfo => methodInfo.GetCustomAttribute<Cmd>())
                .Where(attr => attr != null)
                .Aggregate("", (current, attr) 
                    => current + $"/{attr.Name}: {attr.Desc}\n");
        }

        [Cmd("ip", Order = 1,Desc = "查询当前公网ip")]
        public string Ip(MessageEventArgs e, params string[] args)
        {
            return IpService.GetPublicIp();
        }

        [Cmd("ddns",Order = 2,Desc = "同步DDNS")]
        // ReSharper disable once InconsistentNaming
        public string DDNS(MessageEventArgs e, params string[] args)
        {
            var configService = new ConfigService(new AssistantDbContext(), false);
            return IpService.Ddns(configService.Username,configService.Password,configService.HostName);
        }

        [Cmd("expire", Order = 3, Desc = "过期时间查询")]
        public string Expire(MessageEventArgs e, params string[] args)
        {
            return new V2Service(V2SubLink()).Expire();
        }

        private string V2SubLink()
        {
            using var scope = _provider.CreateScope();
            var configService = scope.ServiceProvider.GetService<ConfigService>();
            if(configService!=null)
                return configService.V2SubLink();
            throw new Exception("获取v2连接失败!");
        }
    }
}