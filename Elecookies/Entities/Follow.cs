using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    //[PrimaryKey(nameof(CustomerId), nameof(ShopId))]
    public class Follow {
        public Guid CustomerId { get; set; }
        public Guid ShopId { get; set; }
        public Customer Customer { get; set; } = null;
        public Shop Shop { get; set; } = null;
    }
}
