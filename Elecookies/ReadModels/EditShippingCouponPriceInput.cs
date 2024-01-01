namespace Elecookies.ReadModels {
    public class EditShippingCouponPriceInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string CouponId { get; set; }
        public int Price { get; set; }
    }
}
