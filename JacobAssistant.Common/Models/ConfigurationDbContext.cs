using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
    }
}