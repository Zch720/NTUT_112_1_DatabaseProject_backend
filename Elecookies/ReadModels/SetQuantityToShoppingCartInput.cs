namespace Elecookies.ReadModels {
    public class SetQuantityToShoppingCartInput {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public int Number { get; set; }
    }
}
