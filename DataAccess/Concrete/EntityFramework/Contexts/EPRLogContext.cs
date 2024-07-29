using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Core.Utilities.Service;
using Microsoft.Extensions.DependencyInjection;
using Entities.Concrete.Entities;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class EPRLogContext : DbContext
    {

        private IConfiguration _configuration;
        public EPRLogContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EPRLog"));
        }
        public DbSet<ApiLog> ApiLogs { get; set; }
        public DbSet<WebLog> WebLogs { get; set; }

    }
    
}
