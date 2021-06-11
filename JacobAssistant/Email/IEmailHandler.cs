using JacobAssistant.Models;

namespace JacobAssistant.Email
{
    public interface IEmailHandler
    {
        public void AddEmailAccount(EmailAccount emailAccount);
        public void ReceiveUpdates();
        public void OnUpdate();
    }
}