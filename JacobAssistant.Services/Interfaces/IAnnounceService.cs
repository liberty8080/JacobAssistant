using JacobAssistant.Common.Models;

namespace JacobAssistant.Services.Interfaces
{
    public interface IAnnounceService
    {
        void Announce(string message);
        // void Announce(string message,BotUser);
        void SendToUser(string message,BotUser user);
    }
}