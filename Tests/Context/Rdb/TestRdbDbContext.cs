using Elecookies;
using Elecookies.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.Context.Rdb {
    public class TestRdbDbContext : DbContext, ElecookiesDbContext {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=elecookies_test;Username=postgres;Password=postgres;");
    }
}