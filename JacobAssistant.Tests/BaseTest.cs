using Serilog;

namespace JacobAssistant.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}