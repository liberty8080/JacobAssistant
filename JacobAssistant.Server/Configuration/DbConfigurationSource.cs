using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Common.Models;
using Microsoft.EntityFrameworkCore;
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

    public class DbConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connStr;

        public DbConfigurationProvider(string connStr)
        {
            _connStr = connStr;
        }

        public override void Load()
        {
            using var context = new ConfigurationDbContext(_connStr);
            // Create the database if it doesn't exist
            context.Database.EnsureCreated();
            var configs = context.Configs;
            Data = configs.Any() ? ToDict(configs) : CreateAndSaveDefaultValues(context);
        }

        public override void Set(string key, string value)
        {
            using var context = new ConfigurationDbContext(_connStr);
            var config = context.Configs.FirstOrDefault(c => c.Name == key);
            if (config == null)
                config = new Config {Name = key, Value = value};
            else
                config.Value = value;
            context.Configs.Update(config);
            context.SaveChanges();
            Load();
        }

        private static Dictionary<string,string> ToDict(DbSet<Config> configs)
        {
            return configs.ToDictionary(e=>e.Name,e=>e.Value);
        }

        private static Dictionary<string, string> CreateAndSaveDefaultValues(ConfigurationDbContext dbContext)
        {


            var keys = ConfigMapping.AllConfigKeys();
            foreach (var key in keys)
            {
                dbContext.Add(new Config {Name = key, Value = ""});
            }
            dbContext.SaveChanges();
            return keys.ToDictionary(item => item, item=>"");

        } 
    }
}