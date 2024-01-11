using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("order_consists_of")]
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderConsistsOf {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public OrderConsistsOf(Guid orderId, Guid productId, int quantity, int unitPrice) {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
