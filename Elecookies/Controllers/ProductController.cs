using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Elecookies.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Elecookies.Controllers {
    [Route("api/product")]
    [ApiController]
    public class ProductController {
        private ProductRepository productRepository;
        private StaffRepository staffRepository;

        public ProductController(ProductRepository productRepository, StaffRepository staffRepository, ShopRepository shopRepository) {
            this.productRepository = productRepository;
            this.staffRepository = staffRepository;
        }

        [Route("create")]
        [HttpPost]
        public string CreateProduct(CreateProductInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Product product = new Product(id, Guid.Parse(input.ShopId), input.Name, input.Stock, input.Price, input.Category, input.Description, input.PublishTime, input.ForSale);
            productRepository.Save(product);

            return product.Id.ToString();
        }

        [Route("delete")]
        [HttpPost]
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

        [Route("image/create")]
        [HttpPost]
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

        [Route("image/delete")]
        [HttpPost]
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

        [Route("edit-name")]
        [HttpPost]
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

        [Route("edit-stock")]
        [HttpPost]
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

        [Route("edit-price")]
        [HttpPost]
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

        [Route("edit-image")]
        [HttpPost]
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

        [Route("edit-description")]
        [HttpPost]
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

        [Route("discount/create")]
        [HttpPost]
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

        [Route("discount/delete")]
        [HttpPost]
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

        [Route("discount/edit-discount-rate")]
        [HttpPost]
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

        [Route("discount/edit-start-time")]
        [HttpPost]
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

        [Route("discount/edit-end-time")]
        [HttpPost]
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

        [Route("products/count")]
        [HttpGet]
        public int GetProductsCount(string productType) {
            try {
                return productRepository.All()
                    .FindAll(p => p.Category.Contains(GetProductCategory(productType)))
                    .Count;
            } catch {
                return 0;
            }
        }

        [Route("products")]
        [HttpGet]
        public List<ProductListItemOutput> GetProducts(string productType, int from, int to) {
            try {
                return productRepository.All()
                    .FindAll(p => p.Category.Contains(GetProductCategory(productType)))
                    .GetRange(from, to - from + 1)
                    .ConvertAll(p => {
                        ProductListItemOutput output = new();
                        output.Id = p.Id.ToString();
                        output.Name = p.Name;
                        output.Price = p.Price;
                        output.Image = p.Images.Count != 0 ? p.Images.ElementAt(0).Image : "";
                        return output;
                    })
                    .ToList();
            } catch {
                return new();
            }
        }

        [Route("get")]
        [HttpGet]
        public ProductDataOutput? GetProduct(string productId) {
            try {
                Product? product = productRepository.FindById(Guid.Parse(productId));
                if (product == null) return null;
                ProductDataOutput output = new ProductDataOutput();
                output.Images = product.Images
                    .OrderBy(image => image.ImageOrder)
                    .ToList()
                    .ConvertAll(image => image.Image);
                output.Name = product.Name;
                output.Price = product.Price;
                output.Stock = product.Stock;
                output.Description = product.Description;
                output.Category = product.Category;
                output.ShopId = product.ShopId.ToString();
                output.ShopName = product.Shop.Name;
                output.ShopLogo = product.Shop.Icon;
                output.ShopDescription = product.Shop.Description;
                return output;
            } catch {
                return null;
            }
        }

        private string GetProductCategory(string engCategoryName) {
            if (engCategoryName == "all") {
                return "";
            }
            else if (engCategoryName == "chocolate-cookie") {
                return "巧克力餅乾";
            }
            else if (engCategoryName == "butter-cookie") {
                return "奶油餅乾";
            }
            else if (engCategoryName == "sandwitch-cookie") {
                return "夾心餅乾";
            }
            else if (engCategoryName == "cookies") {
                return "曲奇餅乾";
            }
            else if (engCategoryName == "soft-cookie") {
                return "美式軟餅乾";
            }
            else if (engCategoryName == "roll-puff-pastry") {
                return "捲心酥";
            }
            else if (engCategoryName == "egg-roll") {
                return "蛋捲";
            }
            else if (engCategoryName == "other") {
                return "其他";
            }
            throw new Exception("Category name is illegal");
        }
    }
}
