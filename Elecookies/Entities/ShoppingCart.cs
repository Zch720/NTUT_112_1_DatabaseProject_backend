using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("shopping_cart")]
    public class ShoppingCart {
        [Key]
        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public List<ShoppingCartHas> ShoppingCartHas { get; set; } = new();
        public ShoppingCart(Guid customerId) {
            CustomerId = customerId;
        }
    }
}
