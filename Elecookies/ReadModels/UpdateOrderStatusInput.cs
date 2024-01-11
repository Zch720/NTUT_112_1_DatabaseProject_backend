namespace Elecookies.ReadModels {
    public class UpdateOrderStatusInput {
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
    }
}
