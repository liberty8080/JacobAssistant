using System;
using JacobAssistant.Bots.Interceptors;
using JacobAssistant.Bots.Messages;
using JacobAssistant.Bots.TgBot;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JacobAssistant.Bots.Extensions
{
    public static class AddBotsExtension
    {
        public static IServiceCollection AddBots(this IServiceCollection service,IConfiguration configuration,IHostEnvironment env)
        {
            Log.Debug("inject Bot to ServiceCollection");
            var options = configuration.GetSection(AppOptions.App).Get<AppOptions>();
            service.AddTransient(_ => new BotOptions(env.IsProduction()?options.TelegramProdBotToken:options.TelegramDevBotToken,
                options.TelegramAdminId,options.TelegramAnnounceChannelId));
            service.AddSingleton<AssistantBotClient,AssistantBotClient>();
            
            // add interceptors
            service.AddSingleton<IMsgInterceptor, PermissionInterceptor>(serviceProvider =>
                new PermissionInterceptor(
                serviceProvider.CreateScope().ServiceProvider.GetService<PermissionService>()));
            service.AddSingleton<InterceptorChain, InterceptorChain>();
            // add dispatcher
            service.AddSingleton<IMessageDispatcher, MessageDispatcher>();
            
            // start the bot
            var provider = service.BuildServiceProvider();
            var botClient = provider.GetService<AssistantBotClient>();
            if (botClient == null) throw new ApplicationException("没有获取到Bot实例");
            botClient.Start();
            return service;
        }
    }
}