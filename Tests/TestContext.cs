using Elecookies;
using Elecookies.Controllers;
using Elecookies.ReadModels;
using Elecookies.Repositories;

namespace Tests {
    public abstract class TestContext {
        public abstract ElecookiesDbContext DbContext { get; }

        public abstract AccountRepository AccountRepository { get; }
        public abstract ShopRepository ShopRepository { get; }
        public abstract StaffRepository StaffRepository { get; }

        public abstract AccountController AccountController { get; }
        public abstract ShopController ShopController { get; }

        public string CreateNewAccount(string loginId, string password, string email) {
            CreateAccountInput input = new CreateAccountInput();
            input.LoginId = loginId;
            input.Password = password;
            input.Email = email;
            input.Name = "Name";
            input.Address = "Address";

            return AccountController.CreateAccount(input);
        }

        public string CreateNewShop() {
            CreateShopInput input = new CreateShopInput();
            input.Name = "shopName";
            input.Address = "address";
            input.Email = "email@gmail.com";
            input.PhoneNumber = "phoneNumber";
            input.Description = "description";

            return ShopController.CreateShop(input);
        }

        public string CreateNewStaff(string loginId, string password, string email) {
            string shopId = CreateNewShop();
            CreateStaffInput input = new CreateStaffInput();
            input.LoginId = loginId;
            input.ShopId = shopId;
            input.Password = password;
            input.Email = email;
            input.Name = "Name";
            input.Address = "Address";

            return AccountController.CreateStaff(input);
        }

    }
}