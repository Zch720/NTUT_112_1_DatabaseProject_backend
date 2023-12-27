namespace Elecookies.Entities {
    public class Staff : Account {
        public Guid ShopId { get; set; }

        public Staff(Guid id, Guid shopId, string loginId, string password, string name, string email, string address) : base(id, loginId, password, name, email, address) {
            ShopId = shopId;
        }
    }
}
