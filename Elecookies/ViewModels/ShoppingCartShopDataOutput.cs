namespace Elecookies.ViewModels {
    public class ShoppingCartShopDataOutput {
        public string ShopId { get; set; } = "";
        public string ShopName { get; set; } = "";
        public string ShopIcon { get; set; } = "";
        public List<ShoppingCartProductDataOutput> Products { get; set; } = new();
    }
}
