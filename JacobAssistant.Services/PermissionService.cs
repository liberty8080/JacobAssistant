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

        public bool CheckPermission(BotUser user,string commandName)
        {
            Log.Information($"Checking Permission... User:{user.UserName},Command:{commandName}");
            var result = from b in _dbContext.BotUsers
                from p in _dbContext.Permissions
                where b.UserId == user.UserId && p.UserId == b.Id
                select p;
            return result.Any();
        }
    }
}