using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Elecookies.Controllers {
    [Route("api/order")]
    [ApiController]
    public class ShopOrderController {
        private ShopOrderRepository shopOrderRepository;
        private CustomerRepository customerRepository;
        private ProductRepository productRepository;
        private ShopRepository shopRepository;
        private CouponRepository couponRepository;

        public ShopOrderController(ShopOrderRepository shopOrderRepository, CustomerRepository customerRepository, ProductRepository productRepository, ShopRepository shopRepository, CouponRepository couponRepository) {
            this.shopOrderRepository = shopOrderRepository;
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.shopRepository = shopRepository;
            this.couponRepository = couponRepository;
        }

        [Route("create")]
        [HttpPost]
        public string CreateShopOrder(CreateShopOrderInput input) {
            if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
                return "";
            }
            if (shopRepository.FindById(Guid.Parse(input.ShopId)) == null) {
                return "";
            }

            Guid id = Guid.NewGuid();
            ShopOrder shopOrder = new ShopOrder(id, Guid.Parse(input.CustomerId), Guid.Parse(input.ShopId), input.Name, input.Phone, input.OrderTime, input.Status, input.Address);
            shopOrderRepository.Save(shopOrder);
            Dictionary<string, int>.KeyCollection productIds = input.Products.Keys;
            foreach (var productId in productIds) {
                Product? product = productRepository.FindById(Guid.Parse(productId));
                if (product != null) {
                    OrderConsistsOf orderConsistsOf = new OrderConsistsOf(id, Guid.Parse(productId), input.Products[productId], input.UnitPrices[productId]);
                    shopOrderRepository.Save(orderConsistsOf);
                    product.ShopOrders.Add(shopOrder);
                    product.OrderConsistsOf.Add(orderConsistsOf);
                    shopOrder.Products.Add(product);
                    shopOrder.OrderConsistsOf.Add(orderConsistsOf);
                    productRepository.Save(product);
                }
            }
            Customer customer = customerRepository.FindById(Guid.Parse(input.CustomerId));
            customer.ShopOrders.Add(shopOrder);
            customerRepository.Save(customer);
            shopOrder.Account = customer;
            Shop shop = shopRepository.FindById(Guid.Parse(input.ShopId));
            shop.ShopOrders.Add(shopOrder);
            shopRepository.Save(shop);
            shopOrder.Shop = shop;
            if (input.ShippingCouponId != null) {
                Coupon? shippingCoupon = couponRepository.FindById(Guid.Parse(input.ShippingCouponId));
                if (shippingCoupon != null) {
                    shopOrder.ShippingCouponId = shippingCoupon.Id;
                    shopOrder.ShippingCoupon = shippingCoupon;
                    shippingCoupon.ShopOrders.Add(shopOrder);
                    couponRepository.Save(shippingCoupon);
                }
            }
            if (input.SeasoningCouponId != null) {
                Coupon? seasoningCoupon = couponRepository.FindById(Guid.Parse(input.SeasoningCouponId));
                if (seasoningCoupon != null) {
                    shopOrder.SeasoningCouponId = seasoningCoupon.Id;
                    shopOrder.SeasoningCoupon = seasoningCoupon;
                    seasoningCoupon.ShopOrders.Add(shopOrder);
                    couponRepository.Save(seasoningCoupon);
                }
            }
            shopOrderRepository.Save(shopOrder);

            return shopOrder.Id.ToString();
        }

        [Route("pay")]
        [HttpPost]
        public void PayOrder(PayOrderInput input) {
            if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
                return;
            }

            ShopOrder? shopOrder = shopOrderRepository.FindById(Guid.Parse(input.OrderId));
            if (shopOrder != null) {
                shopOrder.Status = "Payed";
                shopOrderRepository.Save(shopOrder);
            }
        }

        [Route("cancel")]
        [HttpPost]
        public void CancelOrder(CancelOrderInput input) {
            if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
                return;
            }

            ShopOrder? shopOrder = shopOrderRepository.FindById(Guid.Parse(input.OrderId));
            if (shopOrder != null) {
                shopOrder.Status = "Canceled";
                shopOrderRepository.Save(shopOrder);
            }
        }

        [Route("update-status")]
        [HttpPost]
        public void UpdateOrderStatus(UpdateOrderStatusInput input) {
            if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
                return;
            }

            ShopOrder? shopOrder = shopOrderRepository.FindById(Guid.Parse(input.OrderId));
            if (shopOrder != null) {
                shopOrder.Status = input.Status;
                shopOrderRepository.Save(shopOrder);
            }
        }
    }
}
