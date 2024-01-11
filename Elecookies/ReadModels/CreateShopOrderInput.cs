namespace Elecookies.ReadModels {
    public class CreateShopOrderInput {
        public string CustomerId { get; set; }
        public Dictionary<string, int> Products { get; set; }
        public string ShopId { get; set; }
        public int Quantity { get; set; }
        public string? ShippingCouponId { get; set; }
        public string? SeasoningCouponId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string OrderTime { get; set; }
        public Dictionary<string, int> UnitPrices { get; set; }
    }
}
