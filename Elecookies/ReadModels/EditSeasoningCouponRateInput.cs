namespace Elecookies.ReadModels {
    public class EditSeasoningCouponRateInput {
        public string AccountId { get; set; }
        public string ShopId { get; set; }
        public string CouponId { get; set; }
        public float Rate { get; set; }
    }
}
