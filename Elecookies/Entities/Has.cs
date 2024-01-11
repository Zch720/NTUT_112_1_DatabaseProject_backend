using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("has")]
    [PrimaryKey(nameof(CustomerId), nameof(CouponId))]
    public class Has {
        public Guid CustomerId { get; set; }
        public Guid CouponId { get; set; }
        public int Quantity { get; set; }
        public Has(Guid customerId, Guid couponId, int quantity) {
            CustomerId = customerId;
            CouponId = couponId;
            Quantity = quantity;
        }
    }
}
