using JacobAssistant.Bots.TelegramBots;
using Microsoft.AspNetCore.Builder;

namespace JacobAssistant.Extension
{
    public static class UseBotsExtension
    {
        public static IApplicationBuilder UseBots(this IApplicationBuilder builder,AssistantBotClient client)
        {
            client.Start();
            return builder;
        }
    }
}