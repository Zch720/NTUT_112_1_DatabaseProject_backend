using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Controller {
    public class ShopOrderControllerTest {
        private TestContext context { get; }

        public ShopOrderControllerTest(TestContext testContext) {
            this.context = testContext;
        }

        [TestCleanup]
        public void TearDown() {
            var shopOrders = context.ShopOrderRepository.All();
            var customers = context.CustomerRepository.All();
            var shops = context.ShopRepository.All();
            var coupons = context.CouponRepository.All();
            var products = context.ProductRepository.All();
            foreach (var shop in shops) {
                context.ShopRepository.Delete(shop.Id);
            }
            foreach (var customer in customers) {
                context.CustomerRepository.Delete(customer.Id);
            }
        }

        [TestMethod]
        public void CreateShopOrder() {
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string shippingCouponId = context.CreateNewShippingCoupon(accountId, shopId);
            string seasoningCouponId = context.CreateNewSeasoningCoupon(accountId, shopId);
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

            string id = context.ShopOrderController.CreateShopOrder(input);

            Assert.IsNotNull(context.ShopOrderRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void PayOrder() {
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string shippingCouponId = context.CreateNewShippingCoupon(accountId, shopId);
            string seasoningCouponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            string orderId = context.CreateNewShopOrder(customerId, productId, shopId, shippingCouponId, seasoningCouponId);
            PayOrderInput input = new PayOrderInput();
            input.CustomerId = customerId;
            input.OrderId = orderId;

            context.ShopOrderController.PayOrder(input);
            Assert.AreEqual("Payed", context.ShopOrderRepository.FindById(Guid.Parse(orderId)).Status);
        }

        [TestMethod]
        public void CancelOrder() {
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string shippingCouponId = context.CreateNewShippingCoupon(accountId, shopId);
            string seasoningCouponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            string orderId = context.CreateNewShopOrder(customerId, productId, shopId, shippingCouponId, seasoningCouponId);
            CancelOrderInput input = new CancelOrderInput();
            input.CustomerId = customerId;
            input.OrderId = orderId;

            context.ShopOrderController.CancelOrder(input);
            Assert.AreEqual("Canceled", context.ShopOrderRepository.FindById(Guid.Parse(orderId)).Status);
        }

        [TestMethod]
        public void UpdateOrderStatus() {
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string shippingCouponId = context.CreateNewShippingCoupon(accountId, shopId);
            string seasoningCouponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            string orderId = context.CreateNewShopOrder(customerId, productId, shopId, shippingCouponId, seasoningCouponId);
            UpdateOrderStatusInput input = new UpdateOrderStatusInput();
            input.CustomerId = customerId;
            input.OrderId = orderId;
            input.Status = "newState";

            context.ShopOrderController.UpdateOrderStatus(input);
            Assert.AreEqual("newState", context.ShopOrderRepository.FindById(Guid.Parse(orderId)).Status);
        }
    }
}
