using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("product")]
    [PrimaryKey(nameof(Id))]
    public class Product {
        [Key]
        [Required]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }
        [Column("shopId", TypeName = "uuid")]
        public Guid ShopId { get; set; }
        [Column("name", TypeName = "varchar")]
        public string Name { get; set; }
        [Column("stock", TypeName = "int")]
        public int Stock { get; set; }
        [Column("price", TypeName = "int")]
        public int Price { get; set; }
        [Column("category", TypeName = "varchar")]
        public string Category { get; set; }
        public ICollection<ProductImage> Images { get; } = new List<ProductImage>();
        [Column("description", TypeName = "varchar")]
        public string Description { get; set; }
        [Column("publish_time", TypeName = "varchar")]
        public string PublishTime { get; set; }
        [Column("for_sale", TypeName = "boolean")]
        public bool ForSale { get; set; }
        public ICollection<ProductDiscount> Discounts { get; } = new List<ProductDiscount>();
        public List<ShoppingCart> ShoppingCarts { get; set; } = new();
        public List<ShoppingCartHas> ShoppingCartHas { get; set; } = new();
        public List<ShopOrder> ShopOrders { get; set; } = new();
        public List<OrderConsistsOf> OrderConsistsOf { get; set; } = new();

        public Product(Guid id, Guid shopId, string name, int stock, int price, string category, string description, string publishTime, bool forSale) {
            Id = id;
            ShopId = shopId;
            Name = name;
            Stock = stock;
            Price = price;
            Category = category;
            Description = description;            PublishTime = publishTime;
            ForSale = forSale;
        }
    }
}