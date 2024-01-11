using Elecookies.Database;
using Elecookies.Entities;

namespace Elecookies.Repositories {
    public class CouponRepository : Repository<Coupon, Guid> {
        private ElecookiesDbContext dbContext;

        public CouponRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(Coupon value) {
            if (FindById(value.Id) == null) {
                dbContext.Coupons.Add(value);
            } else {
                dbContext.Coupons.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Delete(Guid id) {
            Coupon? coupon = dbContext.Coupons.Find(id);
            if (coupon != null) {
                dbContext.Coupons.Remove(coupon);
                dbContext.SaveChanges();
            }
        }

        public Coupon? FindById(Guid id) {
            return dbContext.Coupons.Find(id);
        }

        public List<Coupon> All() {
            return dbContext.Coupons.ToList();
        }
    }
}
