namespace Elecookies.ReadModels {
    public class EditProductDiscountEndTimeInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public string DiscountId { get; set; }
        public string EndTime { get; set; }
    }
}
