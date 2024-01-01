namespace Elecookies.ReadModels {
    public class CreateProductInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string PublishTime { get; set; }
        public bool ForSale { get; set; }
    }
}
