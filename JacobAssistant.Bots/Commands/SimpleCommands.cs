﻿// ReSharper disable UnusedMember.Global

using System;
using System.Linq;
using System.Reflection;
using JacobAssistant.Bots.TelegramBots;
using JacobAssistant.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Args;

namespace JacobAssistant.Bots.Commands
{
    [TextCommand]
    public class SimpleCommands
    {
        private readonly IServiceProvider _provider;

        private readonly IConfiguration _configuration;

        // private readonly ConfigService _configService = new(new AssistantDbContext(), false);
        public SimpleCommands(IServiceProvider provider,IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        [Cmd("help", Order = 0, Desc = "帮助")]
        public string Help(MessageEventArgs e, params string[] args)
        {
            var commands = AssistantBotClient.GetCommands().ToList()
                .OrderBy(info => info.GetCustomAttribute<Cmd>()?.Order);
            return commands.Select(methodInfo => methodInfo.GetCustomAttribute<Cmd>())
                .Where(attr => attr != null)
                .Aggregate("", (current, attr)
                    => current + $"/{attr.Name}: {attr.Desc}\n");
        }

        [Cmd("ip", Order = 1, Desc = "查询当前公网ip")]
        public string Ip(MessageEventArgs e, params string[] args)
        {
            return IpService.GetPublicIp();
        }

        //todo: DDNS抽出来,主流的几种都做个实现
        /*[Cmd("ddns", Order = 2, Desc = "同步DDNS")]
        // ReSharper disable once InconsistentNaming
        public string DDNS(MessageEventArgs e, params string[] args)
        {
            return IpService.Ddns(_configuration[""], _configuration[""], _configuration[""]);
        }*/

        /*[Cmd("expire", Order = 3, Desc = "过期时间查询")]
        public string Expire(MessageEventArgs e, params string[] args)
        {
            return new V2Service(V2SubLink()).Expire();
        }*/

        /*[Cmd("wake", Order = 4, Desc = "wake on lan")]
        public string WakeOnLan(MessageEventArgs e, params string[] args)
        {
            string ip;
            ip = args.Length > 0 ? args[0] : "255.255.255.255";
            return WakeOnLanService.WakeUp(_configuration.TargetMac, 9, ip) == 102 ? $"魔术包已发出 ip:{ip}" : "发送失败！";
        }*/

        /*[Cmd("ladder", Order = 5, Desc = "fastlink")]
        public string Ladder(MessageEventArgs e, params string[] args)
        {
            var fastLink = FastLink.GetInstance(_configuration.GetConfig("fastlink_email").Value,
                _configuration.GetConfig("fastlink_passwd").Value);
            return fastLink.GetInfo();
        }*/

        /*private string V2SubLink()
        {
            using var scope = _provider.CreateScope();
            var configService = scope.ServiceProvider.GetService<ConfigService>();
            if (configService != null)
                return configService.V2SubLink();
            throw new Exception("获取v2连接失败!");
        }*/
    }
}