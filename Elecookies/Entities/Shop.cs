using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    public class Shop {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }
        public string Icon { get; set; }
        public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
        public ICollection<ShopOrder> ShopOrders { get; set; } = new List<ShopOrder>();
        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
        public ICollection<Product> Products { get; set; }

        public Shop(Guid id, string name, string address, string email, string phoneNumber, string description) {
            Id = id;
            Name = name;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            Description = description;
        }
    }
}
