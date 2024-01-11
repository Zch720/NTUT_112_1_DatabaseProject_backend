namespace Elecookies.ReadModels {
    public class EditShippingCouponDateInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string CouponId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
