namespace Elecookies.ReadModels {
    public class EditProductStockInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}
