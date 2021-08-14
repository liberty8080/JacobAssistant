using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JacobAssistant.Common.Models
{
    public partial class ConfigurationDbContext : DbContext
    {
        public ConfigurationDbContext()
        {
        }
        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options)
            : base(options)
        {
        }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appsettingsJson = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") switch
            {
                "Development" => "appsettings.Development.json",
                "Development2" => "appsettings.Development2.json",
                _ => "appsettings.json"
            };

            IConfiguration configuration = new ConfigurationBuilder().AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .AddJsonFile(appsettingsJson)
                .Build();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("Mysql"));
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<EmailAccount> EmailAccounts { get; set; }

        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        public virtual DbSet<BotUser> BotUsers { get; set; }
        public virtual DbSet<CommandPermission> Permissions { get; set; }
    }
}