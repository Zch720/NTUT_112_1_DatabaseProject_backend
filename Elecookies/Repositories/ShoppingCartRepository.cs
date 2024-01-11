using Elecookies.Database;
using Elecookies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Elecookies.Repositories {
    public class ShoppingCartRepository : Repository<ShoppingCart, Guid> {
        private ElecookiesDbContext dbContext;

        public ShoppingCartRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(ShoppingCart value) {
            if (FindById(value.CustomerId) == null) {
                dbContext.ShoppingCarts.Add(value);
            }
            else {
                dbContext.ShoppingCarts.Update(value);
            }
            dbContext.SaveChanges();
        }

        //public void Save(ShoppingCartHas value) {
        //    if (FindById(value.CustomerId, value.ProductId) == null) {
        //        dbContext.ShoppingCartHas.Add(value);
        //    }
        //    else {
        //        dbContext.ShoppingCartHas.Update(value);
        //    }
        //    dbContext.SaveChanges();
        //}

        public void Delete(Guid id) {
            ShoppingCart? shoppingCart = dbContext.ShoppingCarts.Find(id);
            if (shoppingCart != null) {
                dbContext.ShoppingCarts.Remove(shoppingCart);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid customerId, Guid productId) {
            ShoppingCartHas? shoppingCartHas = dbContext.ShoppingCartHas.Find(customerId, productId);
            if (shoppingCartHas != null) {
                dbContext.ShoppingCartHas.Remove(shoppingCartHas);
                dbContext.SaveChanges();
            }
        }

        public ShoppingCart? FindById(Guid id) {
            return dbContext.ShoppingCarts
                .Where(e => e.CustomerId == id)
                .Include(e => e.ShoppingCartHas)
                .FirstOrDefault();
        }

        public ShoppingCartHas? FindById(Guid customerId, Guid productId) {
            return dbContext.ShoppingCartHas.Find(customerId, productId);
        }

        public List<ShoppingCart> All() {
            return dbContext.ShoppingCarts.ToList();
        }
    }
}
