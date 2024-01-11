using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("shopping_cart_has")]
    [PrimaryKey(nameof(CustomerId), nameof(ProductId))]
    public class ShoppingCartHas {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public ShoppingCart ShoppintCart { get; set; }

        public ShoppingCartHas(Guid customerId, Guid productId, int quantity) {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
