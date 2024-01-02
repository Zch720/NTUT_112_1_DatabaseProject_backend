using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("product_discount")]
    [PrimaryKey(nameof(Id))]
    public class ProductDiscount {
        [Key]
        [Required]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }
        [Column("product_id", TypeName = "uuid")]
        public Guid ProductId { get; set; }
        [Column("start_time", TypeName = "varchar")]
        public string StartTime { get; set; }
        [Column("end_time", TypeName = "varchar")]
        public string EndTime { get; set; }
        [Column("discount_rate", TypeName = "int")]
        public float DiscountRate { get; set; }
        public Product Product { get; set; } = null!;

        public ProductDiscount(Guid id, Guid productId, string startTime, string endTime, float discountRate) {
            Id = id;
            ProductId = productId;
            StartTime = startTime;
            EndTime = endTime;
            DiscountRate = discountRate;
        }
    }
}
