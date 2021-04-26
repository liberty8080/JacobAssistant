using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Models;
using Microsoft.EntityFrameworkCore;

namespace JacobAssistant.Services
{
    public class ConfigService
    {
        private readonly AssistantDbContext _dbContext;

        public ConfigService(AssistantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Config> GetAll()
        {
           return _dbContext.Configs.ToList();
        }
    }
}