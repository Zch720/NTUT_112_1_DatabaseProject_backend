using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Controller {
    public class ProductControllerTest {
        private TestContext context { get; }

        public ProductControllerTest(TestContext testContext) {
            this.context = testContext;
        }

        [TestCleanup]
        public void TearDown() {
            var products = context.ProductRepository.All();
            var accounts = context.AccountRepository.All();
            var shops = context.ShopRepository.All();
            var productImages = context.ProductRepository.AllImages();
            var productDiscounts = context.ProductRepository.AllDiscounts();
            foreach (var productImage in productImages) {
                context.ProductRepository.DeleteImage(productImage.ProductId, productImage.ImageOrder);
            }
            foreach (var productDiscount in productDiscounts) {
                context.ProductRepository.DeleteDiscount(productDiscount.Id);
            }
            foreach (var product in products) {
                context.ProductRepository.Delete(product.Id);
            }
            foreach (var account in accounts) {
                context.AccountRepository.Delete(account.Id);
            }
            foreach (var shop in shops) {
                context.ShopRepository.Delete(shop.Id);
            }
        }

        [TestMethod]
        public void CreateProduct() {
            CreateProductInput input = new CreateProductInput();
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            input.AccountId = accountId;
            input.ShopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            input.Name = "name";
            input.Stock = 2;
            input.Price = 20;
            input.Category = "category";
            input.Description = "description";
            input.PublishTime = "publishTime";
            input.ForSale = true;

            string id = context.ProductController.CreateProduct(input);

            Assert.IsNotNull(context.ProductRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void DeleteProduct() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            DeleteProductInput input = new DeleteProductInput();
            input.ProductId = productId;
            input.AccountId = accountId;
            input.ShopId = shopId;

            Assert.IsNotNull(context.ProductRepository.FindById(Guid.Parse(productId)));
            bool success = context.ProductController.DeleteProduct(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.ProductRepository.FindById(Guid.Parse(productId)));
        }

        [TestMethod]
        public void CreateProductImage() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            CreateProductImageInput[] productImageInput = new CreateProductImageInput[2];
            for (int i = 0; i < productImageInput.Length; i++) {
                productImageInput[i] = new CreateProductImageInput();
            }
            string[] imageId = new string[2];
            for (int i = 0; i < productImageInput.Length; i++) {
                productImageInput[i].AccountId = accountId;
                productImageInput[i].ShopId = shopId;
                productImageInput[i].ProductId = productId;
                productImageInput[i].ImageOrder = i;
                productImageInput[i].Image = "image" + i.ToString();
                imageId[i] = context.ProductController.CreateProductImage(productImageInput[i]);
            }
            for (int i = 0;i < imageId.Length; i++) {
                string value = imageId[i];
                string id = value.Split('_')[0];
                int imageOrder = int.Parse(value.Split("_")[1]);
                Assert.IsNotNull(context.ProductRepository.FindImageById(Guid.Parse(id), imageOrder));
            }
        }

        [TestMethod]
        public void DeleteProductImage() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string productImage = context.CreateNewProductImage(accountId, shopId, productId, 0);
            DeleteProductImageInput input = new DeleteProductImageInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.ImageOrder = 0;

            Assert.IsNotNull(context.ProductRepository.FindImageById(Guid.Parse(productId), 0));
            bool success = context.ProductController.DeleteProductImage(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.ProductRepository.FindImageById(Guid.Parse(productId), 0));
        }

        [TestMethod]
        public void EditProductName() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            EditProductNameInput input = new EditProductNameInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.Name = "newName";

            context.ProductController.EditProductName(input);

            Assert.AreEqual("newName", context.ProductRepository.FindById(Guid.Parse(productId))!.Name);
        }

        [TestMethod]
        public void EditProductStock() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            EditProductStockInput input = new EditProductStockInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.Stock = 10;

            context.ProductController.EditProductStock(input);

            Assert.AreEqual(10, context.ProductRepository.FindById(Guid.Parse(productId))!.Stock);
        }

        [TestMethod]
        public void EditProductPrice() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            EditProductPriceInput input = new EditProductPriceInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.Price = 100;

            context.ProductController.EditProductPrice(input);

            Assert.AreEqual(100, context.ProductRepository.FindById(Guid.Parse(productId))!.Price);
        }

        [TestMethod]
        public void EditProductImage() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string productImage = context.CreateNewProductImage(accountId, shopId, productId, 0);
            EditProductImageInput input = new EditProductImageInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.ImageOrder = 0;
            input.Image = "newImage";

            context.ProductController.EditProductImage(input);

            Assert.AreEqual("newImage", context.ProductRepository.FindImageById(Guid.Parse(productId), 0)!.Image);
        }

        [TestMethod]
        public void EditProductDescription() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            EditProductDescriptionInput input = new EditProductDescriptionInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.Description = "newDescription";

            context.ProductController.EditProductDescription(input);

            Assert.AreEqual("newDescription", context.ProductRepository.FindById(Guid.Parse(productId))!.Description);
        }

        [TestMethod]
        public void CreateProductDiscount() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            CreateProductDiscountInput input = new CreateProductDiscountInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.StartTime = "start_time";
            input.EndTime = "end_time";
            input.DiscountRate = 0.8F;

            string id = context.ProductController.CreateProductDiscount(input);

            Assert.IsNotNull(context.ProductRepository.FindDiscountById(Guid.Parse(id)));
        }

        [TestMethod]
        public void DeleteProductDiscount() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string discountId = context.CreateNewProductDiscount(accountId, shopId, productId);
            DeleteProductDiscountInput input = new DeleteProductDiscountInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.DiscountId = discountId;

            Assert.IsNotNull(context.ProductRepository.FindDiscountById(Guid.Parse(discountId)));
            bool success = context.ProductController.DeleteProductDiscount(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.ProductRepository.FindDiscountById(Guid.Parse(discountId)));
        }

        [TestMethod]
        public void EditProductDiscountRate() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string discountId = context.CreateNewProductDiscount(accountId, shopId, productId);
            EditProductDiscountRateInput input = new EditProductDiscountRateInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.DiscountId = discountId;
            input.DiscountRate = 0.9F;

            context.ProductController.EditProductDiscountRate(input);

            Assert.AreEqual(0.9F, context.ProductRepository.FindDiscountById(Guid.Parse(discountId))!.DiscountRate);
        }

        [TestMethod]
        public void EditProductDiscountStartTime() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string discountId = context.CreateNewProductDiscount(accountId, shopId, productId);
            EditProductDiscountStartTimeInput input = new EditProductDiscountStartTimeInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.DiscountId = discountId;
            input.StartTime = "newStartTime";

            context.ProductController.EditProductDiscountStartTime(input);

            Assert.AreEqual("newStartTime", context.ProductRepository.FindDiscountById(Guid.Parse(discountId))!.StartTime);
        }

        [TestMethod]
        public void EditProductDiscountEndTime() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string discountId = context.CreateNewProductDiscount(accountId, shopId, productId);
            EditProductDiscountEndTimeInput input = new EditProductDiscountEndTimeInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.DiscountId = discountId;
            input.EndTime = "newEndTime";

            context.ProductController.EditProductDiscountEndTime(input);

            Assert.AreEqual("newEndTime", context.ProductRepository.FindDiscountById(Guid.Parse(discountId))!.EndTime);
        }
    }
}