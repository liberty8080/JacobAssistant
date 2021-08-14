using JacobAssistant.Extension;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace JacobAssistant.Tests
{
    public class BaseTest
    {
        
        protected IConfiguration Configuration { get; set; }
        public BaseTest()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .AddMyConfiguration()
                .Build();
        }
    }
}