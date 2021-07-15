using Microsoft.Extensions.Configuration;

namespace JacobAssistant.Configuration
{
    public class DbConfigurationSource:IConfigurationSource
    {
        private readonly string _connStr;

        public DbConfigurationSource(string connStr)
        {
            _connStr = connStr;
        }
        
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(_connStr);
        }
    }

    //todo: 未完成
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connStr;

        public DbConfigurationProvider(string connStr)
        {
            _connStr = connStr;
        }

        public override void Load()
        {
            
        }
    }
}