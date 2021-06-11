using System.Collections.Generic;
using JacobAssistant.Models;

namespace JacobAssistant.Email
{
    public class BaseEmailHandler:IEmailHandler
    {
        private List<EmailAccount> _accounts = new(); 
        public void AddEmailAccount(EmailAccount emailAccount)
        {
            _accounts.Add(emailAccount);
        }

        public void ReceiveUpdates()
        {
            foreach (var emailAccount in _accounts)
            {

            }
        }

        public void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}