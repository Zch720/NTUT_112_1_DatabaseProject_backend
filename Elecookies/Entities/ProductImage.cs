using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("product_image")]
    [PrimaryKey(nameof(ProductId), nameof(ImageOrder))]
    public class ProductImage {
        [Column("product_id", TypeName = "uuid")]
        public Guid ProductId { get; set; }
        [Column("image_order", TypeName = "int")]
        public int ImageOrder { get; set; }
        [Column("image", TypeName = "varchar")]
        public string Image { get; set; }
        public Product Product { get; set; } = null!;

        public ProductImage(Guid productId, int imageOrder, string image) {
            ProductId = productId;
            ImageOrder = imageOrder;
            Image = image;
        }
    }
}
