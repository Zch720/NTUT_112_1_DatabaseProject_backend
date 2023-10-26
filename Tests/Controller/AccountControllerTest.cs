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
    }
}
