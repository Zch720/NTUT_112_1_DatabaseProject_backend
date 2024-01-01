using Elecookies.ReadModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controller {
    public class AccountControllerTest {
        private TestContext context { get; }

        public AccountControllerTest(TestContext testContext) {
            this.context = testContext;
        }

        [TestCleanup]
        public void TearDown() {
            var accounts = context.AccountRepository.All();
            var staffs = context.StaffRepository.All();
            var customers = context.CustomerRepository.All();
            var shops = context.ShopRepository.All();
            var coupons = context.CouponRepository.All();
            var hases = context.CustomerRepository.AllHas();
            
            foreach (var account in accounts) {
                context.AccountRepository.Delete(account.Id);
            }
            foreach (var staff in staffs) {
                context.StaffRepository.Delete(staff.Id);
            }
            foreach (var customer in customers) {
                context.CustomerRepository.Delete(customer.Id);
            }
            foreach (var shop in shops) {
                context.ShopRepository.Delete(shop.Id);
            }
            foreach (var coupon in coupons) {
                context.CouponRepository.Delete(coupon.Id);
            }
            foreach (var has in hases) {
                context.CustomerRepository.Delete(has.CustomerId, has.CouponId);
            }
        }

        [TestMethod]
        public void CreateAccount() {
            CreateAccountInput input = new CreateAccountInput();
            input.LoginId = "loginId";
            input.Password = "password";
            input.Name = "username";
            input.Email = "email";
            input.Address = "address";

            string id = context.AccountController.CreateAccount(input);

            Assert.IsNotNull(context.AccountRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void UseLoginIdAlreadyExistedWouldFailed() {
            CreateAccountInput input = new CreateAccountInput();
            input.LoginId = "loginId";
            input.Password = "password1";
            input.Name = "username1";
            input.Email = "email1";
            input.Address = "address1";
            context.AccountController.CreateAccount(input);

            input.Password = "password1";
            input.Name = "username2";
            input.Email = "email2";
            input.Address = "address2";
            string id2 = context.AccountController.CreateAccount(input);

            Assert.AreEqual("", id2);
        }

        [TestMethod]
        public void UseEmailAlreadyExistedWouldFailed() {
            CreateAccountInput input = new CreateAccountInput();
            input.LoginId = "loginId1";
            input.Password = "password1";
            input.Name = "username1";
            input.Email = "email";
            input.Address = "address1";
            context.AccountController.CreateAccount(input);

            input.LoginId = "loginId2";
            input.Password = "password2";
            input.Name = "username2";
            input.Address = "address2";
            string id2 = context.AccountController.CreateAccount(input);

            Assert.AreEqual("", id2);
        }

        [TestMethod]
        public void DeleteAccount() {
            string userId = context.CreateNewAccount("LoginId", "Password", "Email");
            DeleteAccountInput input = new DeleteAccountInput();
            input.UserId = userId;
            input.Password = "Password";

            Assert.IsNotNull(context.AccountRepository.FindById(Guid.Parse(userId)));
            bool success = context.AccountController.DeleteAccount(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.AccountRepository.FindById(Guid.Parse(userId)));
        }

        [TestMethod]
        public void DeleteAccountWithWrongPassword() {
            string userId = context.CreateNewAccount("LoginId", "Password", "Email");
            DeleteAccountInput input = new DeleteAccountInput();
            input.UserId = userId;
            input.Password = "WrongPassword";

            Assert.IsNotNull(context.AccountRepository.FindById(Guid.Parse(userId)));
            bool success = context.AccountController.DeleteAccount(input);

            Assert.IsFalse(success);
            Assert.IsNotNull(context.AccountRepository.FindById(Guid.Parse(userId)));
        }

        [TestMethod]
        public void DeleteAccountWithUnexistUserId() {
            string userId = context.CreateNewAccount("LoginId", "Password", "Email");
            DeleteAccountInput input = new DeleteAccountInput();
            input.UserId = new Guid().ToString();
            input.Password = "Password";

            Assert.IsNotNull(context.AccountRepository.FindById(Guid.Parse(userId)));
            bool success = context.AccountController.DeleteAccount(input);

            Assert.IsFalse(success);
            Assert.IsNotNull(context.AccountRepository.FindById(Guid.Parse(userId)));
        }

        [TestMethod]
        public void EditAccountName() {
            string userId = context.CreateNewAccount("LoginId", "Password", "Email");
            EditAccountNameInput input = new EditAccountNameInput();
            input.UserId = userId;
            input.Name = "NewName";

            context.AccountController.EditAccountName(input);

            Assert.AreEqual("NewName", context.AccountRepository.FindById(Guid.Parse(userId)).Name);
        }

        [TestMethod]
        public void EditAccountPassword() {
            string userId = context.CreateNewAccount("LoginId", "Password", "Email");
            EditAccountPasswordInput input = new EditAccountPasswordInput();
            input.UserId = userId;
            input.Password = "Password";
            input.NewPassword = "NewPassword";

            context.AccountController.EditAccountPassword(input);

            Assert.AreEqual("NewPassword", context.AccountRepository.FindById(Guid.Parse(userId)).Password);
        }


        [TestMethod]
        public void EditAccountEmail() {
            string userId = context.CreateNewAccount("LoginId", "Password", "Email");
            EditAccountEmailInput input = new EditAccountEmailInput();
            input.UserId = userId;
            input.Email = "NewEmail";

            context.AccountController.EditAccountEmail(input);

            Assert.AreEqual("NewEmail", context.AccountRepository.FindById(Guid.Parse(userId)).Email);
        }

        [TestMethod]
        public void EditAccountAddress() {
            string userId = context.CreateNewAccount("LoginId", "Password", "Email");
            EditAccountAddressInput input = new EditAccountAddressInput();
            input.UserId = userId;
            input.Address = "NewAddress";

            context.AccountController.EditAccountAddress(input);

            Assert.AreEqual("NewAddress", context.AccountRepository.FindById(Guid.Parse(userId)).Address);
        }

        [TestMethod]
        public void AccountLoginWithLoginId() {
            string userId = context.CreateNewAccount("test0123", "testpassword", "test@gmail.com");
            LoginInput input = new LoginInput();
            input.UserAccount = "test0123";
            input.Password = "testpassword";

            string result = context.AccountController.Login(input);

            Assert.AreNotEqual("", result);
            Assert.AreEqual(userId, result);
        }

        [TestMethod]
        public void AccountLoginWithEmail() {
            string userId = context.CreateNewAccount("test0123", "testpassword", "test@gmail.com");
            LoginInput input = new LoginInput();
            input.UserAccount = "test@gmail.com";
            input.Password = "testpassword";

            string result = context.AccountController.Login(input);

            Assert.AreNotEqual("", result);
            Assert.AreEqual(userId, result);
        }

        [TestMethod]
        public void CreateCustomer() {
            CreateCustomerInput input = new CreateCustomerInput();
            input.LoginId = "loginId";
            input.Password = "password";
            input.Name = "name";
            input.Email = "email";
            input.Address = "address";

            string id = context.AccountController.CreateCustomer(input);
            

            Assert.IsNotNull(context.CustomerRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void DelteCustomer() {
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");

            DeleteCustomerInput input = new DeleteCustomerInput();
            input.UserId = customerId;
            input.Password = "Password";

            Assert.IsNotNull(context.CustomerRepository.FindById(Guid.Parse(customerId)));
            bool success = context.AccountController.DeleteCustomer(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.CustomerRepository.FindById(Guid.Parse(customerId)));
        }

        [TestMethod]
        public void FollowShop() {
            string shopId = context.CreateNewShop();
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            FollowShopInput input = new FollowShopInput();
            input.CustomerId = customerId;
            input.ShopId = shopId;

            context.AccountController.FollowShop(input);
            Assert.AreEqual(1, context.CustomerRepository.FindById(Guid.Parse(customerId)).Shops.Count);
            Assert.AreEqual(1, context.ShopRepository.FindById(Guid.Parse(shopId)).Customers.Count);
        }

        [TestMethod]
        public void AddCoupon() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string couponId = context.CreateNewSeasoningCoupon(accountId, shopId);
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            AddCouponInput input = new AddCouponInput();
            input.CustomerId = customerId;
            input.CouponId = couponId;

            context.AccountController.AddCoupon(input);
            Assert.AreEqual(1, context.CustomerRepository.FindById(Guid.Parse(customerId)).Coupons.Count);
            Assert.AreEqual(1, context.CustomerRepository.FindById(Guid.Parse(customerId), Guid.Parse(couponId)).Quantity);

            context.AccountController.AddCoupon(input);
            Assert.AreEqual(1, context.CustomerRepository.FindById(Guid.Parse(customerId)).Coupons.Count);
            Assert.AreEqual(2, context.CustomerRepository.FindById(Guid.Parse(customerId), Guid.Parse(couponId)).Quantity);
        }
    }
}
