using System.Net.Http;
using static JacobAssistant.Services.HttpClientService;

namespace JacobAssistant.Services
{
    public static class IpService
    {
        public static string GetPublicIp()
        {
           return  GetAsync("http://ip.42.pl/raw").GetAwaiter().GetResult();
        }


        public static string Ddns(string username,string password,string hostname)
        {
            return GetAsync($"https://api.dynu.com/nic/update?hostname={hostname}&myip={GetPublicIp()}&username={username}&password={password}").GetAwaiter().GetResult();
        }
    }
}