using Elecookies;
using Microsoft.EntityFrameworkCore;

namespace Tests.Context.InMemory {
    public class InMemoryDbContext : DbContext, ElecookiesDbContext {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) {
        }
    }
}