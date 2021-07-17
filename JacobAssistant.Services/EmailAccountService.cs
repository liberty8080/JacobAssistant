using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Models;
using Microsoft.Extensions.Configuration;

namespace JacobAssistant.Services
{
    public class EmailAccountService
    {
        // private ConfigurationDbContext _context;
        private ConfigurationDbContext _configuration;
        /*public EmailAccountService()
        {
            // _context = new AssistantDbContext();
        }*/

        public EmailAccountService(ConfigurationDbContext context)
        {
            _configuration = context;
        }

        // 查询所有Email账户
        public List<EmailAccount> EmailAccounts()
        {
            return _configuration.EmailAccounts.ToList();
        }

        public EmailAccount GetAOutlook()
        {
            return _configuration.EmailAccounts.FirstOrDefault(account => account.State == 1 && account.Type == 1);
        }

        public EmailAccount GetAGmail()
        {
            return _configuration.EmailAccounts.FirstOrDefault(account => account.State == 1 && account.Type == 2);
        }
    }
}