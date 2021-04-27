using JacobAssistant.Bot;
using JacobAssistant.Bot.core;
using JacobAssistant.Services;
using Microsoft.AspNetCore.Mvc;

namespace JacobAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private AssistantBotClient bot;
        private readonly ConfigService _configService;

        public BotController(AssistantBotClient bot,ConfigService configService)
        {
            this.bot = bot;
            _configService = configService;
        }

        [HttpGet("reload")]
        public void ReloadOptions()=> bot.ReloadOptions(_configService.BotOptions());
        
    }
}