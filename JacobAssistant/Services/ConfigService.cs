using System;
using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Bot;
using JacobAssistant.Models;

namespace JacobAssistant.Services
{
    public class ConfigService
    {
        private const string DevBotTokenName = "dev_bot_token";
        public const string ProdBotTokenName = "prod_bot_token";
        public const string AnnounceChannelIdName = "announce_channel_id";
        public const string AdminIdName = "admin_id";
        private readonly AssistantDbContext _dbContext;
        private readonly bool _isDev;

        public ConfigService(AssistantDbContext dbContext,bool isDev)
        {
            _dbContext = dbContext;
            _isDev = isDev;
        }

        public List<Config> GetAll()
        {
            return _dbContext.Configs.ToList();
        }

        public Config GetConfig(string name)
        {
            return _dbContext.Configs.FirstOrDefault(cf=>cf.Name.Equals(name));
        }

        private string GetToken()
        {
            if (_isDev)
            {
                return GetConfig(DevBotTokenName).Value;
            }
            
            return GetConfig(ProdBotTokenName).Value;
        }

        private long AnnounceChannel()
        {
            return Convert.ToInt64(GetConfig(AnnounceChannelIdName).Value);
        }

        private long GetAdminId()
        {
            return Convert.ToInt64(GetConfig(AdminIdName).Value);
        }

        public BotOptions BotOptions()
        {
            return new(GetToken(), GetAdminId(),AnnounceChannel() );
        }
        
    }
}