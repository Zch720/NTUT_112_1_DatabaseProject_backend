using Elecookies.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Elecookies.Database {
    public class RdbContext : DbContext, ElecookiesDbContext {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCartHas> ShoppingCartHas { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Has> Has { get; set; }
        public DbSet<ShopOrder> ShopOrders { get; set; }
        public DbSet<OrderConsistsOf> OrderConsistsOfs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=elecookies;Username=postgres;Password=postgres;");
    }
}
