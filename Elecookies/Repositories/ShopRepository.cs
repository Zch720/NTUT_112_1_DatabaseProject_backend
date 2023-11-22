using Elecookies.Database;
using Elecookies.Entities;

namespace Elecookies.Repositories {
    public class ShopRepository : Repository<Shop, Guid> {
        private ElecookiesDbContext dbContext;

        public ShopRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(Shop value) {
            if (FindById(value.Id) == null) {
                dbContext.Shops.Add(value);
            } else {
                dbContext.Shops.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Delete(Guid id) {
            Shop? shop = dbContext.Shops.Find(id);
            if (shop != null) {
                dbContext.Shops.Remove(shop);
                dbContext.SaveChanges();
            }
        }

        public Shop? FindById(Guid id) {
            return dbContext.Shops.Find(id);
        }

        public List<Shop> All() {
            return dbContext.Shops.ToList();
        }
    }
}
