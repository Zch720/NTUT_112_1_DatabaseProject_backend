namespace Elecookies.ReadModels {
    public class EditProductDiscountRateInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public string DiscountId { get; set; }
        public float DiscountRate { get; set; }
    }
}
