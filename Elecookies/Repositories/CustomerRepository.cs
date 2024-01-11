using Elecookies.Database;
using Elecookies.Entities;
using Elecookies.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Elecookies.Repositories {
    public class CustomerRepository : Repository<Customer, Guid> {
        private ElecookiesDbContext dbContext;

        public CustomerRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(Customer value) {
            if (FindById(value.Id) == null) {
                dbContext.Customers.Add(value);
            } else {
                dbContext.Customers.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Save(Has value) {
            if (FindById(value.CustomerId, value.CouponId) == null) {
                dbContext.Has.Add(value);
            } else {
                dbContext.Has.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Delete(Guid id) {
            Customer? customer = dbContext.Customers.Find(id);
            if (customer != null) {
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid customerId, Guid couponId) {
            Has? has= dbContext.Has.Find(customerId, couponId);
            if (has != null) {
                dbContext.Has.Remove(has);
                dbContext.SaveChanges();
            }
        }

        public Customer? FindById(Guid id) {
            Customer? customer = dbContext.Customers.Find(id);
            if (customer == null) return null;
            customer.Shops = dbContext.Customers
                .Where(c => c.Id == id)
                .SelectMany(c => c.Shops)
                .ToList();
            return customer;
        }

        public Has? FindById(Guid customerId, Guid couponId) {
            return dbContext.Has.Find(customerId, couponId);
        }

        public List<Customer> All() {
            return dbContext.Customers.ToList();
        }

        public List<Has> AllHas() {
            return dbContext.Has.ToList();
        }

        public List<Shop> GetCustomerFollows(Guid customerId) {
            try {
                if (FindById(customerId) != null) {
                    return dbContext.Customers
                        .Where(c => c.Id == customerId)
                        .SelectMany(c => c.Shops)
                        .ToList();
                }
            }
            catch (Exception ex) {
            }
            return new();
        }

        public void FollowShop(Guid customerId, Guid shopId) {
            try {
                Customer customer = dbContext.Customers.Include(c => c.Shops)
                    .Single(c => c.Id == customerId);
                Shop shop = dbContext.Shops.Single(s => s.Id == shopId);
                customer.Shops.Add(shop);
                dbContext.Customers.Update(customer);

                dbContext.SaveChanges();
            } catch {
                Debug.WriteLine("error");
            }
        }

        public void UnfollowShop(Guid customerId, Guid shopId) {
            try {
                Customer customer = dbContext.Customers.Include(c => c.Shops).Single(c => c.Id == customerId);
                Shop shop = customer.Shops.Single(s => s.Id == shopId);
                customer.Shops.Remove(shop);
                dbContext.SaveChanges();
            } catch {
                Debug.WriteLine("error");
            }
        }
    }
}
