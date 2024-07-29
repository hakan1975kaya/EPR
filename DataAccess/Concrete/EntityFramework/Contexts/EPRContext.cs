using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Service;
using Microsoft.Extensions.DependencyInjection;
using Entities.Concrete.Entities;
using System;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class EPRContext : DbContext
    {
        private IConfiguration _configuration;
        public EPRContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EPR"));
        }
        public DbSet<Corporate> Corporates { get; set; }
        public DbSet<CorporateMailAddress> CorporateMailAddresses { get; set; }
        public DbSet<HcpUpload> HcpUploads { get; set; }
        public DbSet<MenuOperationClaim> MenuOperationClaims { get; set; }
        public DbSet<MailAddress> MailAddresses { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<PaymentRequestDetail> PaymentRequestDetails { get; set; }
        public DbSet<PaymentRequestSummary> PaymentRequestSummaries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleOperationClaim> RoleOperationClaims { get; set; }
        public DbSet<SftpDownload> SftpDownloads { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

