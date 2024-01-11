using Elecookies.ReadModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Controller {
    public class ShopControllerTest {
        private TestContext context { get; }

        public ShopControllerTest(TestContext testContext) {
            this.context = testContext;
        }

        [TestCleanup]
        public void TearDown() {
            var shops = context.ShopRepository.All();
            var staffs = context.StaffRepository.All();
            foreach (var shop in shops) {
                context.ShopRepository.Delete(shop.Id);
            }
            foreach (var staff in staffs) {
                context.StaffRepository.Delete(staff.Id);
            }
        }

        [TestMethod]
        public void CreateShop() {
            CreateShopInput input = new CreateShopInput();
            input.Name = "shopName";
            input.Address = "address";
            input.Email = "email@gmail.com";
            input.PhoneNumber = "phoneNumber";
            input.Description = "description";

            string id = context.ShopController.CreateShop(input);

            Assert.IsNotNull(context.ShopRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void CreateStaff() {
            string shopId = context.CreateNewShop();
            CreateStaffInput input = new CreateStaffInput();
            input.LoginId = "loginId";
            // Todo: AccountId
            //input.AccountId = "accountId";
            input.ShopId = shopId;
            input.Password = "password";
            input.Name = "username";
            input.Email = "email";
            input.Address = "address";

            string id = context.AccountController.CreateStaff(input);

            Assert.IsNotNull(context.StaffRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void DeleteStaff() {
            string staffId = context.CreateNewStaff("LoginId", "Password", "Email");

            DeleteStaffInput input = new DeleteStaffInput();
            input.Id = staffId;
            // Todo: AccountId
            //input.AccountId = userId;

            Assert.IsNotNull(context.StaffRepository.FindById(Guid.Parse(staffId)));
            bool success = context.AccountController.DeleteStaff(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.StaffRepository.FindById(Guid.Parse(staffId)));
        }

        [TestMethod]
        public void DeleteShop() {
            string shopId = context.CreateNewShop();
            DeleteShopInput input = new DeleteShopInput();
            input.ShopId = shopId;
            // Todo: AccountId
            //input.AccountId = userId;
            
            Assert.IsNotNull(context.ShopRepository.FindById(Guid.Parse(shopId)));
            bool success = context.ShopController.DeleteShop(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.ShopRepository.FindById(Guid.Parse(shopId)));
        }

        [TestMethod]
        public void EditShopName() {
            string id = context.CreateNewShop();
            // AccountId
            EditShopNameInput input = new EditShopNameInput();
            input.Id = id;
            input.Name = "newName";

            context.ShopController.EditShopName(input);

            Assert.AreEqual("newName", context.ShopRepository.FindById(Guid.Parse(id)).Name);
        }

        [TestMethod]
        public void EditShopAddress() {
            string id = context.CreateNewShop();
            EditShopAddressInput input = new EditShopAddressInput();
            input.Id = id;
            input.Address = "newAddress";

            context.ShopController.EditShopAddress(input);

            Assert.AreEqual("newAddress", context.ShopRepository.FindById(Guid.Parse(id)).Address);
        }

        [TestMethod]
        public void EditShopEmail() {
            string id = context.CreateNewShop();
            EditShopEmailInput input = new EditShopEmailInput();
            input.Id = id;
            input.Email = "newEmail@gmail.com";

            context.ShopController.EditShopEmail(input);

            Assert.AreEqual("newEmail@gmail.com", context.ShopRepository.FindById(Guid.Parse(id)).Email);
        }

        [TestMethod]
        public void EditShopPhoneNumber() {
            string id = context.CreateNewShop();
            EditShopPhoneNumberInput input = new EditShopPhoneNumberInput();
            input.Id = id;
            input.Phone = "newPhoneNumber";

            context.ShopController.EditShopPhoneNumber(input);

            Assert.AreEqual("newPhoneNumber", context.ShopRepository.FindById(Guid.Parse(id)).PhoneNumber);
        }

        [TestMethod]
        public void EditShopDescription() {
            string id = context.CreateNewShop();
            EditShopDescriptionInput input = new EditShopDescriptionInput();
            input.Id = id;
            input.Description = "newDescription";

            context.ShopController.EditShopDescription(input);

            Assert.AreEqual("newDescription", context.ShopRepository.FindById(Guid.Parse(id)).Description);
        }
    }
}
