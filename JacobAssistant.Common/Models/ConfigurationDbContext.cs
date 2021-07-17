using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JacobAssistant.Common.Models
{
    public partial class ConfigurationDbContext : DbContext
    {
        private readonly string _connStr;

  

        public ConfigurationDbContext(string connStr)
        {
            _connStr = connStr;
        }

        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<EmailAccount> EmailAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseMySQL(_connStr);
        }

 

    }
}