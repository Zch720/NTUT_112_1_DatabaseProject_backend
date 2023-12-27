using Elecookies.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elecookies {
    public interface ElecookiesDbContext {
        DbSet<Account> Accounts { get; set; }
        DbSet<Shop> Shops { get; set; }
        DbSet<Staff> Staffs { get; set; }

        int SaveChanges();
    }
}
