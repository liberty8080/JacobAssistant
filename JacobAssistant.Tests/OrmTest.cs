using JacobAssistant.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;

namespace JacobAssistant.Tests
{
    public class OrmTest
    {
        protected DbContextOptions<AssistantDbContext> Options { get; }

        public OrmTest(DbContextOptions<AssistantDbContext> dbContextOptions)
        {
            Options = dbContextOptions;
            Seed();
        }

        /**
         * 测试化测试数据库
         */
        public void Seed()
        {
            using var context = new AssistantDbContext(Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            AddData();
        }

        private void AddData()
        {
            
        }
        
    }
}