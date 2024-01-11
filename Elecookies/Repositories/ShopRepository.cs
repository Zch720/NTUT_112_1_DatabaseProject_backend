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
            Shop? shop = dbContext.Shops.Find(id);
            if (shop == null) return null;
            shop.Customers = dbContext.Shops
                .Where(s => s.Id == id)
                .SelectMany(s => s.Customers)
                .ToList();
            return shop;
        }

        public List<Shop> All() {
            return dbContext.Shops.ToList();
        }

        public List<Product> GetProducts(Guid id) {
            try {
                if (FindById(id) != null) {
                    return dbContext.Shops
                        .Where(s => s.Id == id)
                        .SelectMany(s => s.Products)
                        .ToList();
                }
            }
            catch {
            }
            return new();
        }
    }
}
