namespace Elecookies.ReadModels {
    public class EditProductImageInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public int ImageOrder { get; set; }
        public string Image { get; set; }
    }
}
