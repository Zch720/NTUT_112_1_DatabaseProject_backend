using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;

namespace Elecookies.Controllers {
    public class ProductController {
        private ProductRepository productRepository;
        private StaffRepository staffRepository;

        public ProductController(ProductRepository productRepository, StaffRepository staffRepository, ShopRepository shopRepository) {
            this.productRepository = productRepository;
            this.staffRepository = staffRepository;
        }

        public string CreateProduct(CreateProductInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Product product = new Product(id, Guid.Parse(input.ShopId), input.Name, input.Stock, input.Price, input.Category, input.Description, input.PublishTime, input.ForSale);
            productRepository.Save(product);

            return product.Id.ToString();
        }

        public bool DeleteProduct(DeleteProductInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }

            Product? product = productRepository.FindById(Guid.Parse(input.ProductId));
            if (product != null) {
                List<ProductImage> images = productRepository.AllImages().FindAll(images => images.ProductId == product.Id);
                foreach (ProductImage image in images) {
                    productRepository.DeleteImage(image.ProductId, image.ImageOrder);
                }
                List<ProductDiscount> discounts = productRepository.AllDiscounts().FindAll(discount => discount.ProductId == product.Id);
                foreach (ProductDiscount discount in discounts) {
                    productRepository.DeleteDiscount(discount.Id);
                }
                productRepository.Delete(product.Id);
                return true;
            }
            return false;
        }

        public string CreateProductImage(CreateProductImageInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId)) == null) {
                return "";
            }

            ProductImage productImage = new ProductImage(Guid.Parse(input.ProductId), input.ImageOrder, input.Image);
            productImage.Product = productRepository.FindById(productImage.ProductId)!;
            Product product = productRepository.FindById(Guid.Parse(input.ProductId))!;
            productRepository.SaveImage(productImage);
            product.Images.Add(productImage);
            productRepository.Save(product);

            return productImage.ProductId + "_" + productImage.ImageOrder.ToString();
        }

        public bool DeleteProductImage(DeleteProductImageInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }

            ProductImage? productImage = productRepository.FindImageById(Guid.Parse(input.ProductId), input.ImageOrder);
            if (productImage != null) {
                Product product = productRepository.FindById(productImage.ProductId)!;
                product.Images.Remove(productImage);
                productRepository.Save(product);
                productRepository.DeleteImage(Guid.Parse(input.ProductId), input.ImageOrder);
                return true;
            }
            return false;
        }

        public void EditProductName(EditProductNameInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Product? product = productRepository.FindById(Guid.Parse(input.ProductId));
            if (product != null) {
                product.Name = input.Name;
                productRepository.Save(product);
            }
        }

        public void EditProductStock(EditProductStockInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Product? product = productRepository.FindById(Guid.Parse(input.ProductId));
            if (product != null) {
                product.Stock = input.Stock;
                productRepository.Save(product);
            }
        }

        public void EditProductPrice(EditProductPriceInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Product? product = productRepository.FindById(Guid.Parse(input.ProductId));
            if (product != null) {
                product.Price = input.Price;
                productRepository.Save(product);
            }
        }

        public void EditProductImage(EditProductImageInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            ProductImage? productImage = productRepository.FindImageById(Guid.Parse(input.ProductId), input.ImageOrder);
            if (productImage != null) {
                productImage.Image = input.Image;
                productRepository.SaveImage(productImage);
            }
        }

        public void EditProductDescription(EditProductDescriptionInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Product? product = productRepository.FindById(Guid.Parse(input.ProductId));
            if (product != null) {
                product.Description = input.Description;
                productRepository.Save(product);
            }
        }

        public string CreateProductDiscount(CreateProductDiscountInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId)) == null) {
                return "";
            }

            Guid id = Guid.NewGuid();
            ProductDiscount productDiscount = new ProductDiscount(id, Guid.Parse(input.ProductId), input.StartTime, input.EndTime, input.DiscountRate);
            productDiscount.Product = productRepository.FindById(productDiscount.ProductId)!;
            Product? product = productRepository.FindById(Guid.Parse(input.ProductId))!;
            productRepository.SaveDiscount(productDiscount);
            product.Discounts.Add(productDiscount);
            productRepository.Save(product);

            return productDiscount.Id.ToString();
        }

        public bool DeleteProductDiscount(DeleteProductDiscountInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }
            if (productRepository.FindDiscountById(Guid.Parse(input.DiscountId))?.ProductId != Guid.Parse(input.ProductId)) {
                return false;
            }

            ProductDiscount? productDiscount = productRepository.FindDiscountById(Guid.Parse(input.DiscountId));
            if (productDiscount != null) {
                Product product = productRepository.FindById(productDiscount.ProductId)!;
                product.Discounts.Remove(productDiscount);
                productRepository.Save(product);
                productRepository.DeleteDiscount(productDiscount.Id);
                return true;
            }
            return false;
        }

        public void EditProductDiscountRate(EditProductDiscountRateInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindDiscountById(Guid.Parse(input.DiscountId))?.ProductId != Guid.Parse(input.ProductId)) {
                return;
            }

            ProductDiscount? productDiscount = productRepository.FindDiscountById(Guid.Parse(input.DiscountId));
            if (productDiscount != null) {
                productDiscount.DiscountRate = input.DiscountRate;
                productRepository.SaveDiscount(productDiscount);
            }
        }

        public void EditProductDiscountStartTime(EditProductDiscountStartTimeInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindDiscountById(Guid.Parse(input.DiscountId))?.ProductId != Guid.Parse(input.ProductId)) {
                return;
            }

            ProductDiscount? productDiscount = productRepository.FindDiscountById(Guid.Parse(input.DiscountId));
            if (productDiscount != null) {
                productDiscount.StartTime = input.StartTime;
                productRepository.SaveDiscount(productDiscount);
            }
        }

        public void EditProductDiscountEndTime(EditProductDiscountEndTimeInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (productRepository.FindDiscountById(Guid.Parse(input.DiscountId))?.ProductId != Guid.Parse(input.ProductId)) {
                return;
            }

            ProductDiscount? productDiscount = productRepository.FindDiscountById(Guid.Parse(input.DiscountId));
            if (productDiscount != null) {
                productDiscount.EndTime = input.EndTime;
                productRepository.SaveDiscount(productDiscount);
            }
        }
    }
}
