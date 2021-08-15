using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Models;
using Serilog;

namespace JacobAssistant.Services
{
    public class PermissionService
    {
        private readonly ConfigurationDbContext _dbContext;

        public PermissionService(ConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual bool CheckPermission(BotUser user, string commandName)
        {
            Log.Information($"Checking Permission... ");
            var query = from b in _dbContext.BotUsers.Where(item => item.UserId == user.UserId)
                from p in _dbContext.Permissions.Where(item => item.CommandName == commandName)
                where b.Id == p.UserId && p.Permission == 1
                select p;

            if (query.Any())
            {
                Log.Information($"Permission Passed User:{user.UserName},Command:{commandName}");
                return true;
            }
            else
            {
                Log.Information($"Permission Denied！User:{user.UserName},Command:{commandName}");
                return false;
            }
        }
    }
}