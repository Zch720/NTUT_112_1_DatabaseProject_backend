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
            foreach (var account in accounts) {
                context.AccountRepository.Delete(account.Id);
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
    }
}
