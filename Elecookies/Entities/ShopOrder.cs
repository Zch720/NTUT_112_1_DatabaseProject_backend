using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    public class ShopOrder {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid? ShippingCouponId { get; set; }
        public Guid? SeasoningCouponId { get; set; }
        public Guid ShopId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string OrderTime { get; set; }
        public string? ShipTime { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public Customer Account { get; set; } = null!;
        [NotMapped]
        public Coupon? ShippingCoupon { get; set; }
        [NotMapped]
        public Coupon? SeasoningCoupon { get; set; }
        public Shop Shop { get; set; } = null!;
        public List<Product> Products { get; } = new();
        public List<OrderConsistsOf> OrderConsistsOf { get; } = new();

        public ShopOrder(Guid id, Guid accountId, Guid shopId, string name, string phone, string orderTime, string status, string address) {
            Id = id;
            AccountId = accountId;
            ShopId = shopId;
            Name = name;
            Phone = phone;
            OrderTime = orderTime;
            Status = status;
            Address = address;
        }
    }
}
