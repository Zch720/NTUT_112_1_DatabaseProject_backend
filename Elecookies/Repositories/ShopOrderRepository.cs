using Elecookies.Database;
using Elecookies.Entities;

namespace Elecookies.Repositories {
    public class ShopOrderRepository : Repository<ShopOrder, Guid> {
        private ElecookiesDbContext dbContext;

        public ShopOrderRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(ShopOrder value) {
            if (FindById(value.Id) == null) {
                dbContext.ShopOrders.Add(value);
            } else {
                dbContext.ShopOrders.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Save(OrderConsistsOf value) {
            if (FindById(value.OrderId, value.ProductId) == null) {
                dbContext.OrderConsistsOfs.Add(value);
            }
            else {
                dbContext.OrderConsistsOfs.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void Delete(Guid id) {
            ShopOrder? shopOrder = dbContext.ShopOrders.Find(id);
            if (shopOrder != null) {
                dbContext.ShopOrders.Remove(shopOrder);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid orderId, Guid productId) {
            OrderConsistsOf? orderConsistsOf = dbContext.OrderConsistsOfs.Find(orderId, productId);
            if (orderConsistsOf != null) {
                dbContext.OrderConsistsOfs.Remove(orderConsistsOf);
                dbContext.SaveChanges();
            }
        }

        public ShopOrder? FindById(Guid id) {
            return dbContext.ShopOrders.Find(id);
        }

        public OrderConsistsOf? FindById(Guid orderId, Guid productId) {
            return dbContext.OrderConsistsOfs.Find(orderId, productId);
        }

        public List<ShopOrder> All() {
            return dbContext.ShopOrders.ToList();
        }
        public List<OrderConsistsOf> AllOrderConsistsOf() {
            return dbContext.OrderConsistsOfs.ToList();
        }
    }
}
