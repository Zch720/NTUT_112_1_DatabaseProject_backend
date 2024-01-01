namespace Elecookies.ReadModels {
    public class CreateShippingCouponInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
