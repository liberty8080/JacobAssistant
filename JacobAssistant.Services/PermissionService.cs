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
            return _dbContext.Permissions.Any(
                item => item.UserId == user.UserId
                        &&item.CommandName==commandName);
        }
    }
}