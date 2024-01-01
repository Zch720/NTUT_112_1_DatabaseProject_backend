namespace Elecookies.ReadModels {
    public class EditProductPriceInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public int Price { get; set; }
    }
}
