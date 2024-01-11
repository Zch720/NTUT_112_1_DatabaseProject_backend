using Elecookies.Entities;
using Elecookies.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Elecookies {
    public interface ElecookiesDbContext {
        DbSet<Account> Accounts { get; set; }
        DbSet<Shop> Shops { get; set; }
        DbSet<Staff> Staffs { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<ProductDiscount> ProductDiscounts { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<ShoppingCartHas> ShoppingCartHas { get; set; }
        DbSet<Coupon> Coupons { get; set; }
        DbSet<Has> Has { get; set; }
        DbSet<ShopOrder> ShopOrders { get; set; }
        DbSet<OrderConsistsOf> OrderConsistsOfs { get; set; }

        int SaveChanges();
    }
}
