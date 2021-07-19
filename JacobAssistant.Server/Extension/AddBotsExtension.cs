using JacobAssistant.Bots.TelegramBots;
using JacobAssistant.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace JacobAssistant.Extension
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
            return service;
        }
    }
}