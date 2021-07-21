using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JacobAssistant.Common.Models
{
    public partial class ConfigurationDbContext : DbContext
    {
        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<EmailAccount> EmailAccounts { get; set; }
        
    }
}