using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Models;

namespace JacobAssistant.Services
{
    public class EmailAccountService
    {
        private AssistantDbContext _context;
        
        public EmailAccountService()
        {
            _context = new AssistantDbContext();
        }

        public EmailAccountService(AssistantDbContext context)
        {
            _context = context;
        }

        public List<EmailAccount> EmailAccounts()
        {
          return  _context.EmailAccounts.ToList();
        }

        public EmailAccount GetAOutlook()
        {
            return _context.EmailAccounts.FirstOrDefault(account => account.State == 1 && account.Type == 1);
        }

        public EmailAccount GetAGmail()
        {
            return _context.EmailAccounts.FirstOrDefault(account => account.State == 1 && account.Type == 2);
        }
        
        
    }
}