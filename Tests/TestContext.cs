using Elecookies;
using Elecookies.Controllers;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Tests.Controller;

namespace Tests {
    public abstract class TestContext {
        public abstract ElecookiesDbContext DbContext { get; }

        public abstract AccountRepository AccountRepository { get; }
        public abstract ShopRepository ShopRepository { get; }
        public abstract StaffRepository StaffRepository { get; }
        public abstract ProductRepository ProductRepository { get; }
        public abstract ShoppingCartRepository ShoppingCartRepository { get; }
        public abstract CustomerRepository CustomerRepository { get; }
        public abstract CouponRepository CouponRepository { get; }
        public abstract ShopOrderRepository ShopOrderRepository { get; }

        public abstract AccountController AccountController { get; }
        public abstract ShopController ShopController { get; }
        public abstract ProductController ProductController { get; }
        public abstract ShoppingCartController ShoppingCartController { get; }
        public abstract CouponController CouponController { get; }
        public abstract ShopOrderController ShopOrderController { get; }


        public string CreateNewAccount(string loginId, string password, string email) {
            CreateAccountInput input = new CreateAccountInput();
            input.LoginId = loginId;
            input.Password = password;
            input.Email = email;
            input.Name = "Name";
            input.Address = "Address";

            return AccountController.CreateAccount(input);
        }

        public string CreateNewShop() {
            CreateShopInput input = new CreateShopInput();
            input.Name = "shopName";
            input.Address = "address";
            input.Email = "email@gmail.com";
            input.PhoneNumber = "phoneNumber";
            input.Description = "description";

            return ShopController.CreateShop(input);
        }

        public string CreateNewStaff(string loginId, string password, string email) {
            string shopId = CreateNewShop();
            CreateStaffInput input = new CreateStaffInput();
            input.LoginId = loginId;
            input.ShopId = shopId;
            input.Password = password;
            input.Email = email;
            input.Name = "Name";
            input.Address = "Address";

            return AccountController.CreateStaff(input);
        }

        public string CreateNewProduct(string accountId, string shopId) {
            CreateProductInput input = new CreateProductInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.Name = "name";
            input.Stock = 20;
            input.Price = 200;
            input.Category = "category";
            input.Description = "description";
            input.PublishTime = "publistTime";
            input.ForSale = true;

            return ProductController.CreateProduct(input);
        }

        public string CreateNewProductImage(string accountId, string shopId, string productId, int imageOrder) {
            CreateProductImageInput input = new CreateProductImageInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.ImageOrder = imageOrder;
            input.Image = "image" + imageOrder.ToString();
            return ProductController.CreateProductImage(input);
        }

        public string CreateNewProductDiscount(string accountId, string shopId, string productId) {
            CreateProductDiscountInput input = new CreateProductDiscountInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.ProductId = productId;
            input.StartTime = "startTime";
            input.EndTime = "endTime";
            input.DiscountRate = 0.8F;

            return ProductController.CreateProductDiscount(input);
        }

        public string CreateNewCustomer(string loginId, string password, string email) {
            CreateCustomerInput input = new CreateCustomerInput();
            input.LoginId = loginId;
            input.Password = password;
            input.Email = email;
            input.Name = "Name";
            input.Address = "Address";

            return AccountController.CreateCustomer(input);
        }

        public string CreateNewShoppingCart(string customerId) {
            CreateShoppingCartInput input = new CreateShoppingCartInput();
            input.CustomerId = customerId;

            return ShoppingCartController.CreateShoppingCart(input);
        }

        public void AddNewToShoppingCart(string customerId, string productId) {
            string shoppingCartId = CreateNewShoppingCart(customerId);
            SetQuantityToShoppingCartInput input = new SetQuantityToShoppingCartInput();
            input.CustomerId = customerId;
            input.ProductId = productId;
            input.Number = 3;

            ShoppingCartController.SetQuantityToShoppingCart(input);
        }

        public string CreateNewShippingCoupon(string accountId, string shopId) {
            CreateShippingCouponInput input = new CreateShippingCouponInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.Name = "name";
            input.Price = 50;
            input.StartTime = "startTime";
            input.EndTime = "endTime";

            return CouponController.CreateShippingCoupon(input);
        }
        public string CreateNewSeasoningCoupon(string accountId, string shopId) {
            CreateSeasoningCouponInput input = new CreateSeasoningCouponInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.Name = "name";
            input.Price = 50;
            input.StartTime = "startTime";
            input.EndTime = "endTime";

            return CouponController.CreateSeasoningCoupon(input);
        }
        public string CreateNewShopOrder(string customerId, string productId, string shopId, string shippingCouponId, string seasoningCouponId) {
            Dictionary<string, int> products = new Dictionary<string, int> {
                { productId, 3 }
            };
            Dictionary<string, int> productPrices = new Dictionary<string, int> {
                { productId, 200 }
            };
            CreateShopOrderInput input = new CreateShopOrderInput();
            input.CustomerId = customerId;
            input.Products = products;
            input.ShopId = shopId;
            input.Quantity = 3;
            input.ShippingCouponId = shippingCouponId;
            input.SeasoningCouponId = seasoningCouponId;
            input.Name = "name";
            input.Phone = "phone";
            input.Address = "address";
            input.Status = "status";
            input.OrderTime = "orderTime";
            input.UnitPrices = productPrices;

            return ShopOrderController.CreateShopOrder(input);
        }
    }
}