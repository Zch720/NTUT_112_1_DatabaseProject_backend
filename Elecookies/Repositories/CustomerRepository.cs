using Elecookies.Database;
using Elecookies.Entities;

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
            return dbContext.Customers.Find(id);
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
    }
}
