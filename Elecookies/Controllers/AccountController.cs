using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Elecookies.Controllers {
    [Route("api/user")]
    [ApiController]
    public class AccountController {
        private AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

        [Route("create")]
        [HttpPost]
        public string CreateAccount(CreateAccountInput input) {
            if (accountRepository.All().Find(account => account.LoginId == input.LoginId) != null) {
                return "";
            }
            if (accountRepository.All().Find(account => account.Email == input.Email) != null) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Account account = new Account(id, input.LoginId, input.Password, input.Name, input.Email, input.Address);

            accountRepository.Save(account);

            return account.Id.ToString();
        }

        [Route("delete")]
        [HttpPost]
        public bool DeleteAccount(DeleteAccountInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null && account.Password == input.Password) {
                accountRepository.Delete(account.Id);
                return true;
            }
            return false;
        }

        [Route("edit-name")]
        [HttpPost]
        public void EditAccountName(EditAccountNameInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Name = input.Name;
                accountRepository.Save(account);
            }
        }

        [Route("edit-password")]
        [HttpPost]
        public void EditAccountPassword(EditAccountPasswordInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null && account.Password == input.Password) {
                account.Password = input.NewPassword;
                accountRepository.Save(account);
            }
        }

        [Route("edit-email")]
        [HttpPost]
        public void EditAccountEmail(EditAccountEmailInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Email = input.Email;
                accountRepository.Save(account);
            }
        }

        [Route("edit-address")]
        [HttpPost]
        public void EditAccountAddress(EditAccountAddressInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null){
                account.Address = input.Address;
                accountRepository.Save(account);
            }
        }

        [Route("signin")]
        [HttpPost]
        public string Login(LoginInput input) {
            Account? account = accountRepository.All().Find(account => {
                return account.LoginId == input.UserAccount && account.Password == input.Password;
            });
            if (account == null) {
                account = accountRepository.All().Find(account => {
                    return account.Email == input.UserAccount && account.Password == input.Password;
                });
            }

            if (account != null) {
                return account.Id.ToString();
            }
            return "";
        }
    }
}
