using Microsoft.EntityFrameworkCore;

namespace Elecookies.Database {
    public class ElecookiesDbContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=elecookies;Username=postgres;Password=postgres;");
    }
}
