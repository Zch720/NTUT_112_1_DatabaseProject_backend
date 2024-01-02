using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("Coupon")]
    public class Coupon {
        [Key]
        [Required]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Column("shop_id", TypeName = "uuid")]
        public Guid ShopId { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("start_time", TypeName = "varchar")]
        public string StartTime { get; set; }

        [Column("end_time", TypeName = "varchar")]
        public string EndTime { get; set; }

        [Column("cost_lower_bound", TypeName = "integer")]
        public int CostLowerBound { get; set; }

        [Column("type", TypeName = "varchar(9)")]
        public string Type { get; set; }

        [Column("discount_rate", TypeName = "double precision")]
        public float? DiscountRate { get; set; }

        public Shop Shop { get; set; } = null!;
        public List<Customer> Customers { get; } = new();
        public List<Has> Has { get; } = new();
        [NotMapped]
        public ICollection<ShopOrder> ShopOrders { get; } = new List<ShopOrder>();

        public Coupon(Guid id, Guid shopId, string name, string startTime, string endTime, int costLowerBound, string type) {
            Id = id;
            ShopId = shopId;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            CostLowerBound = costLowerBound;
            Type = type;
        }
    }
}
