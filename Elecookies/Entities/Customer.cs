using System.ComponentModel.DataAnnotations;

namespace Elecookies.Entities {
    public class Customer : Account {
        public ShoppingCart? ShoppingCart { get; set; }
        public ICollection<Shop> Shops { get; set; } = new List<Shop>();
        //public ICollection<Follow> Follows { get; set; }
        public List<Coupon> Coupons { get; } = new();
        public List<Has> Has { get; } = new();
        public List<ShopOrder> ShopOrders { get; } = new();

        public Customer(Guid id, string loginId, string password, string name, string email, string address) : base(id, loginId, password, name, email, address) {
        }
    }
}
