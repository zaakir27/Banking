using Banking.Api.Models.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Banking.Api.Models.Configurations
{
    public class ApiDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<Transfer> Transfers { get; set; }
    }
}
