using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    public class Coupon {
        public Guid Id { get; set; }
        public Guid ShopId { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CostLowerBound { get; set; }
        public string Type { get; set; }
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
