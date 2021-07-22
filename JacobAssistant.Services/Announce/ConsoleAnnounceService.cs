using JacobAssistant.Services.Interfaces;
using Serilog;

namespace JacobAssistant.Services.Announce
{
    public class ConsoleAnnounceService:IAnnounceService
    {
        public void Announce(string message)
        {
            Log.Information(message);
        }
    }
}