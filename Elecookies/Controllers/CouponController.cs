using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;

namespace Elecookies.Controllers {
    public class CouponController {
        private CouponRepository couponRepository;
        private StaffRepository staffRepository;
        private ShopRepository shopRepository;

        public CouponController(CouponRepository couponRepository, StaffRepository staffRepository, ShopRepository shopRepository) {
            this.couponRepository = couponRepository;
            this.staffRepository = staffRepository;
            this.shopRepository = shopRepository;
        }

        public string CreateShippingCoupon(CreateShippingCouponInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Coupon? coupon = new Coupon(id, Guid.Parse(input.ShopId), input.Name, input.StartTime, input.EndTime, input.Price, "Shipping");
            coupon.Shop = shopRepository.FindById(coupon.ShopId);
            couponRepository.Save(coupon);
            Shop? shop = shopRepository.FindById(coupon.ShopId);
            shop.Coupons.Add(coupon);
            shopRepository.Save(shop);

            return coupon.Id.ToString();
        }

        public void EditShippingCouponPrice(EditShippingCouponPriceInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (couponRepository.FindById(Guid.Parse(input.CouponId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (coupon != null) {
                coupon.CostLowerBound = input.Price;
                couponRepository.Save(coupon);
            }
        } 

        public void EditShippingCouponDate(EditShippingCouponDateInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (couponRepository.FindById(Guid.Parse(input.CouponId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (coupon != null) {
                coupon.StartTime = input.StartTime;
                coupon.EndTime = input.EndTime;
                couponRepository.Save(coupon);
            }
        }

        public bool DeleteShippingCoupon(DeleteShippingCouponInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }
            if (couponRepository.FindById(Guid.Parse(input.CouponId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }

            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (coupon != null) {
                Shop? shop = shopRepository.FindById(coupon.ShopId);
                shop.Coupons.Remove(coupon);
                shopRepository.Save(shop);
                couponRepository.Delete(coupon.Id);
                return true;
            }
            return false;
        }
        
        public string CreateSeasoningCoupon(CreateSeasoningCouponInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Coupon? coupon = new Coupon(id, Guid.Parse(input.ShopId), input.Name, input.StartTime, input.EndTime, input.Price, "Seasoning");
            coupon.DiscountRate = input.DiscountRate;
            coupon.Shop = shopRepository.FindById(coupon.ShopId);
            couponRepository.Save(coupon);
            Shop? shop = shopRepository.FindById(coupon.ShopId);
            shop.Coupons.Add(coupon);
            shopRepository.Save(shop);

            return coupon.Id.ToString();
        }

        public void EditSeasoningCouponPrice(EditSeasoningCouponPriceInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (couponRepository.FindById(Guid.Parse(input.CouponId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (coupon != null) {
                coupon.CostLowerBound = input.Price;
                couponRepository.Save(coupon);
            }
        }

        public void EditSeasoningCouponDate(EditSeasoningCouponDateInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (couponRepository.FindById(Guid.Parse(input.CouponId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (coupon != null) {
                coupon.StartTime = input.StartTime;
                coupon.EndTime = input.EndTime;
                couponRepository.Save(coupon);
            }
        } 

        public void EditSeasoningCouponRate(EditSeasoningCouponRateInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }
            if (couponRepository.FindById(Guid.Parse(input.CouponId))?.ShopId != Guid.Parse(input.ShopId)) {
                return;
            }

            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (coupon != null) {
                coupon.DiscountRate = input.Rate;
                couponRepository.Save(coupon);
            }
        }

        public bool DeleteSeasoningCoupon(DeleteSeasoningCouponInput input) {
            if (staffRepository.FindById(Guid.Parse(input.AccountId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }
            if (couponRepository.FindById(Guid.Parse(input.CouponId))?.ShopId != Guid.Parse(input.ShopId)) {
                return false;
            }

            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (coupon != null) {
                Shop? shop = shopRepository.FindById(coupon.ShopId);
                shop.Coupons.Remove(coupon);
                shopRepository.Save(shop);
                couponRepository.Delete(coupon.Id);
                return true;
            }
            return false;
        }
    }
}
