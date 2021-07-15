using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JacobAssistant.Common.Models
{
    public partial class AssistantDbContext : DbContext
    {
        private readonly string _connStr;

        public AssistantDbContext()
        {
        }

        public AssistantDbContext(string connStr)
        {
            _connStr = connStr;
        }

        public AssistantDbContext(DbContextOptions<AssistantDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<ConfigType> ConfigTypes { get; set; }
        public virtual DbSet<EmailAccount> EmailAccounts { get; set; }

        //todo: todo
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            
            
            if (!string.IsNullOrEmpty(_connStr))
            {
                optionsBuilder.UseMySql(_connStr, ServerVersion.Parse("8.0.22-mysql"));
            }
            else
            {
                optionsBuilder.UseSqlite();
            }
        }

 

    }
}