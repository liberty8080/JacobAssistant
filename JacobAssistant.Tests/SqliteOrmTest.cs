using JacobAssistant.Models;
using JacobAssistant.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JacobAssistant.Tests
{
    public class SqliteOrmTest :OrmTest
    {
        public SqliteOrmTest() : base(
            new DbContextOptionsBuilder<AssistantDbContext>().UseSqlite("Filename=Test.db").Options)
        {
        }


        [Fact]
        public void Can_get_item()
        {
            using (var context = new AssistantDbContext(Options))
            {
                var service = new ConfigService(context);
                var configs = service.GetAll();
                Assert.NotEmpty(configs);
            }
        }
    }
}