namespace Elecookies.ReadModels {
    public class DeleteProductImageInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public int ImageOrder { get; set; }
    }
}
