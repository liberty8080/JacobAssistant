using System.Collections.Generic;
using System.Linq;
using JacobAssistant.Common.Configuration;
using JacobAssistant.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace JacobAssistant.Configuration
{
    public class DbConfigurationSource : IConfigurationSource
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
            using var context = new ConfigurationDbContext(
                new DbContextOptionsBuilder<ConfigurationDbContext>().UseMySQL(_connStr).Options);
            // Create the database if it doesn't exist
            context.Database.EnsureCreated();
            var configs = context.Configs;
            var options = typeof(AppOptions).GetProperties();
            // 如果AppOptions新增属性，则数据库也相应新增
            foreach (var info in options)
            {
                if (!ConfContains(configs, info.Name))
                {
                    Log.Warning($"没有检测到该配置项，生成默认！ ：{info.Name}");
                    // 生成默认配置项
                    CreateMissingConfig(context, info.Name);
                    Data.Add($"{AppOptions.App}:{info.Name}", null);
                }
                else
                {
                    var c = configs.First(item => item.Name == info.Name);
                    Data.Add($"{AppOptions.App}:{c.Name}", c.Value);
                }
            }

            context.SaveChanges();
            // Data = configs.Any() ? ToDict(configs) : CreateAndSaveDefaultValues(context);
        }
        /// <summary>
        /// 修改数据库中的配置项，并刷新
        /// todo:待实现
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void Set(string key, string value)
        {
            /*Log.Debug($"set {key} as {value}");
            using var context = new ConfigurationDbContext(_connStr);
            var config = context.Configs.FirstOrDefault(c => c.Name == key);
            if (config == null)
                config = new Config {Name = key, Value = value};
            else
                config.Value = value;
            context.Configs.Update(config);
            context.SaveChanges();
            
            Data.Clear();
            Load();*/
            base.Set(key,value);
        }


        /// <summary>
        /// 向数据库中添加缺少的配置字段
        /// </summary>
        /// <param name="dbContext">dbContext</param>
        /// <param name="name">待添加字段</param>
        private static void CreateMissingConfig(ConfigurationDbContext dbContext, string name)
        {
            dbContext.Configs.Add(new Config {Name = name});
        }


        /// <summary>
        /// 检查表中是否有该字段
        /// </summary>
        /// <param name="configs">表中所有字段</param>
        /// <param name="name">查询字段</param>
        /// <returns></returns>
        private static bool ConfContains(IEnumerable<Config> configs, string name)
        {
            bool result = false;
            foreach (var config in configs)
            {
                if (config.Name == name) result = true;
            }

            return result;
        }
    }
}