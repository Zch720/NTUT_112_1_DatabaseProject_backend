using Elecookies;
using Elecookies.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests.Context.InMemory {
    public class InMemoryDbContext : DbContext, ElecookiesDbContext {
        public DbSet<Account> Accounts { get; set; }

        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) {
        }
    }
}