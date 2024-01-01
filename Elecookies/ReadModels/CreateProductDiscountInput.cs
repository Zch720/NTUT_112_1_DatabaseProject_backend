namespace Elecookies.ReadModels {
    public class CreateProductDiscountInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public float DiscountRate { get; set; }
    }
}
