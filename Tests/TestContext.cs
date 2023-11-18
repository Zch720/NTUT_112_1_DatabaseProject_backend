using Elecookies;
using Elecookies.Controllers;
using Elecookies.ReadModels;
using Elecookies.Repositories;

namespace Tests {
    public abstract class TestContext {
        public abstract ElecookiesDbContext DbContext { get; }

        public abstract AccountRepository AccountRepository { get; }

        public abstract AccountController AccountController { get; }

        public string CreateNewAccount(string loginId, string password, string email) {
            CreateAccountInput input = new CreateAccountInput();
            input.LoginId = loginId;
            input.Password = password;
            input.Email = email;
            input.Name = "Name";
            input.Address = "Address";

            return AccountController.CreateAccount(input);
        }
    }
}