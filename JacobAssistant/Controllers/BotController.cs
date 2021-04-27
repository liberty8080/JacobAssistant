using JacobAssistant.Bot.core;
using JacobAssistant.Services;
using Microsoft.AspNetCore.Mvc;

namespace JacobAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ConfigService _configService;
        private readonly AssistantBotClient _bot;

        public BotController(AssistantBotClient bot, ConfigService configService)
        {
            _bot = bot;
            _configService = configService;
        }

        [HttpGet("reload")]
        public void ReloadOptions()
        {
            _bot.ReloadOptions(_configService.BotOptions());
        }
    }
}