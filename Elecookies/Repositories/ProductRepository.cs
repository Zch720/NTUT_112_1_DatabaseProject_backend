using Elecookies.Database;
using Elecookies.Entities;

namespace Elecookies.Repositories {
    public class ProductRepository : Repository<Product, Guid> {
        private ElecookiesDbContext dbContext;

        public ProductRepository(ElecookiesDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public void Save(Product value) {
            if (FindById(value.Id) == null) {
                dbContext.Products.Add(value);
            } else {
                dbContext.Products.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void SaveImage(ProductImage value) {
            if (FindImageById(value.ProductId, value.ImageOrder) == null) {
                dbContext.ProductImages.Add(value);
            } else {
                dbContext.ProductImages.Update(value);
            }
            dbContext.SaveChanges();
        }

        public void SaveDiscount(ProductDiscount value) {
            if (FindDiscountById(value.Id) == null) {
                dbContext.ProductDiscounts.Add(value);
            } else {
                dbContext.ProductDiscounts.Update(value);
            }
            dbContext.SaveChanges();
        }


        public void Delete(Guid id) {
            Product? product = dbContext.Products.Find(id);
            if (product != null) {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
        }

        public void DeleteImage(Guid productId, int imageOrder) {
            ProductImage? productImage = dbContext.ProductImages.Find(productId, imageOrder);
            if (productImage != null) {
                dbContext.ProductImages.Remove(productImage);
                dbContext.SaveChanges();
            }
        }

        public void DeleteDiscount(Guid id) {
            ProductDiscount? productDiscount = dbContext.ProductDiscounts.Find(id);
            if (productDiscount != null) {
                dbContext.ProductDiscounts.Remove(productDiscount);
                dbContext.SaveChanges();
            }
        }


        public Product? FindById(Guid id) {
            return dbContext.Products.Find(id);
        }

        public ProductImage? FindImageById(Guid productId, int imageOrder) {
            return dbContext.ProductImages.Find(productId, imageOrder);
        }

        public ProductDiscount? FindDiscountById(Guid id) {
            return dbContext.ProductDiscounts.Find(id);
        }

        public List<Product> All() {
            return dbContext.Products.ToList();
        }

        public List<ProductImage> AllImages() {
            return dbContext.ProductImages.ToList();
        }

        public List<ProductDiscount> AllDiscounts() {
            return dbContext.ProductDiscounts.ToList();
        }
    }
}
