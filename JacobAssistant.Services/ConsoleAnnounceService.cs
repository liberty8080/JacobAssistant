using JacobAssistant.Services.Interfaces;
using Serilog;

namespace JacobAssistant.Services
{
    public class ConsoleAnnounceService:IAnnounceService
    {
        public void Announce(string message)
        {
            Log.Information($"Announced Message");
        }
    }
}