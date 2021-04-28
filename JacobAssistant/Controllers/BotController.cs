using System;
using System.Net.Http;
using System.Threading.Tasks;
using JacobAssistant.Bot;
using JacobAssistant.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

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
        public void ReloadOptions() => _bot.ReloadOptions(_configService.BotOptions());


        [HttpGet("sendMsg")]
        public async Task<Message> SendMsgToJacob(string msg)
        {
            try
            {
                return await _bot.SendMessageToJacob(msg);
                
            }
            catch (HttpRequestException)
            {
                return new Message();
            }
        }
    }
}