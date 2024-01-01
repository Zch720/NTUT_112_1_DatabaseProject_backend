using Elecookies.ReadModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Tests.Controller {
    public class CouponControllerTest {
        private TestContext context { get; }

        public  CouponControllerTest(TestContext testContext) {
            this.context = testContext;
        }

        [TestCleanup]
        public void TearDown() {
            var coupons = context.CouponRepository.All();
            var customers = context.CustomerRepository.All();
            var staffs = context.StaffRepository.All();
            var shops = context.ShopRepository.All();
            foreach (var coupon in coupons) {
                context.CouponRepository.Delete(coupon.Id);
            }
            foreach (var customer in customers) {
                context.CustomerRepository.Delete(customer.Id);
            }
            foreach (var staff in staffs) {
                context.StaffRepository.Delete(staff.Id);
            }
            foreach (var shop in shops) {
                context.ShopRepository.Delete(shop.Id);
            }
        }

        [TestMethod]
        public void CreateShippingCoupon() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            CreateShippingCouponInput input = new CreateShippingCouponInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.Name = "name";
            input.Price = 50;
            input.StartTime = "startTime";
            input.EndTime = "endTime";

            string id = context.CouponController.CreateShippingCoupon(input);
            Assert.IsNotNull(context.CouponRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void EditShippingCouponPrice() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewShippingCoupon(accountId, shopId);
            EditShippingCouponPriceInput input = new EditShippingCouponPriceInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.CouponId = couponId;
            input.Price = 70;

            context.CouponController.EditShippingCouponPrice(input);
            Assert.AreEqual(70, context.CouponRepository.FindById(Guid.Parse(couponId)).CostLowerBound);
        }

        [TestMethod]
        public void EditShippingCouponDate() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewShippingCoupon(accountId, shopId);
            EditShippingCouponDateInput input = new EditShippingCouponDateInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.CouponId = couponId;
            input.StartTime = "newStartTime";
            input.EndTime = "newEndTime";

            context.CouponController.EditShippingCouponDate(input);
            Assert.AreEqual("newStartTime", context.CouponRepository.FindById(Guid.Parse(couponId)).StartTime);
            Assert.AreEqual("newEndTime", context.CouponRepository.FindById(Guid.Parse(couponId)).EndTime);
        }

        [TestMethod]
        public void DeleteShippingCoupon() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewShippingCoupon(accountId, shopId);
            DeleteShippingCouponInput input = new DeleteShippingCouponInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.CouponId = couponId;

            Assert.IsNotNull(context.CouponRepository.FindById(Guid.Parse(couponId)));
            bool success = context.CouponController.DeleteShippingCoupon(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.CouponRepository.FindById(Guid.Parse(couponId)));
        }

        [TestMethod]
        public void CreateSeasoningCoupon() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            CreateSeasoningCouponInput input = new CreateSeasoningCouponInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.Name = "name";
            input.Price = 50;
            input.StartTime = "startTime";
            input.EndTime = "endTime";
            input.DiscountRate = 0.8F;

            string id = context.CouponController.CreateSeasoningCoupon(input);
            Assert.IsNotNull(context.CouponRepository.FindById(Guid.Parse(id)));
            Assert.AreEqual("Seasoning", context.CouponRepository.FindById(Guid.Parse(id)).Type);
        }

        [TestMethod]
        public void EditSeasoningCouponPrice() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            EditSeasoningCouponPriceInput input = new EditSeasoningCouponPriceInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.CouponId = couponId;
            input.Price = 70;

            context.CouponController.EditSeasoningCouponPrice(input);
            Assert.AreEqual(70, context.CouponRepository.FindById(Guid.Parse(couponId)).CostLowerBound);
        }

        [TestMethod]
        public void EditSeasoningCouponDate() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            EditSeasoningCouponDateInput input = new EditSeasoningCouponDateInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.CouponId = couponId;
            input.StartTime = "newStartTime";
            input.EndTime = "newEndTime";

            context.CouponController.EditSeasoningCouponDate(input);
            Assert.AreEqual("newStartTime", context.CouponRepository.FindById(Guid.Parse(couponId)).StartTime);
            Assert.AreEqual("newEndTime", context.CouponRepository.FindById(Guid.Parse(couponId)).EndTime);
        }

        [TestMethod]
        public void EditSeasoningCouponRate() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            EditSeasoningCouponRateInput input = new EditSeasoningCouponRateInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.CouponId = couponId;
            input.Rate = 0.6F;

            context.CouponController.EditSeasoningCouponRate(input);
            Assert.AreEqual(0.6F, context.CouponRepository.FindById(Guid.Parse(couponId)).DiscountRate);
        }

        [TestMethod]
        public void DeleteSeasoningCoupon() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            DeleteSeasoningCouponInput input = new DeleteSeasoningCouponInput();
            input.AccountId = accountId;
            input.ShopId = shopId;
            input.CouponId = couponId;

            Assert.IsNotNull(context.CouponRepository.FindById(Guid.Parse(couponId)));
            bool success = context.CouponController.DeleteSeasoningCoupon(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.CouponRepository.FindById(Guid.Parse(couponId)));
        }
    }
}
