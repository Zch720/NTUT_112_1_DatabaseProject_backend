using Elecookies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Shops)
                .WithMany()
                .UsingEntity<Follow>(
                    "Follows",
                    f => f.HasOne(e => e.Shop).WithMany().HasForeignKey(e => e.ShopId),
                    f => f.HasOne(e => e.Customer).WithMany().HasForeignKey(e => e.CustomerId)
                );
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Coupons)
                .WithMany(e => e.Customers)
                .UsingEntity(
                    l => l.HasOne(typeof(Coupon)).WithMany().OnDelete(DeleteBehavior.SetNull),
                    r => r.HasOne(typeof(Customer)).WithMany().OnDelete(DeleteBehavior.Cascade));
            modelBuilder.Entity<ShopOrder>()
                .HasOne(e => e.Account)
                .WithMany(e => e.ShopOrders)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Shop)
                .HasForeignKey(e => e.ShopId)
                .IsRequired();
            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Staffs)
                .WithOne(e => e.Shop)
                .HasForeignKey(e => e.ShopId)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .HasOne(e => e.Shop)
                .WithMany(e => e.Products);
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Images)
                .WithOne(e => e.Product);
            modelBuilder.Entity<ShoppingCart>()
                .HasMany(e => e.ShoppingCartHas)
                .WithOne(e => e.ShoppintCart);
            modelBuilder.Entity<Product>()
                .HasMany(e => e.ShoppingCartHas)
                .WithOne(e => e.Product);
        }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=elecookies;Username=postgres;Password=postgres;");
    }
}
