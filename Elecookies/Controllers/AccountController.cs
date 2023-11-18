using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Elecookies.Controllers {
    [ApiController]
    public class AccountController {
        private AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository) {
            this.accountRepository = accountRepository;
        }

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

        public bool DeleteAccount(DeleteAccountInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null && account.Password == input.Password) {
                accountRepository.Delete(account.Id);
                return true;
            }
            return false;
        }

        public void EditAccountName(EditAccountNameInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Name = input.Name;
                accountRepository.Save(account);
            }
        }

        public void EditAccountPassword(EditAccountPasswordInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null && account.Password == input.Password) {
                account.Password = input.NewPassword;
                accountRepository.Save(account);
            }
        }

        public void EditAccountEmail(EditAccountEmailInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Email = input.Email;
                accountRepository.Save(account);
            }
        }

        public void EditAccountAddress(EditAccountAddressInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null){
                account.Address = input.Address;
                accountRepository.Save(account);
            }
        }

    }
}
