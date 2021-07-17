using System.Net.Http;
using System.Threading.Tasks;

namespace JacobAssistant.Services
{
    public static class HttpClientService
    {
        public static async Task<string> GetAsync(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}