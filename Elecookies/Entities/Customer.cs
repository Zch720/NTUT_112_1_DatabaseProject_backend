namespace Elecookies.Entities {
    public class Customer : Account {
        public ShoppingCart? ShoppingCart { get; set; }
        public List<Shop> Shops { get; } = new();
        public List<Coupon> Coupons { get; } = new();
        public List<Has> Has { get; } = new();
        public List<ShopOrder> ShopOrders { get; } = new();

        public Customer(Guid id, string loginId, string password, string name, string email, string address) : base(id, loginId, password, name, email, address) {
        }
    }
}
