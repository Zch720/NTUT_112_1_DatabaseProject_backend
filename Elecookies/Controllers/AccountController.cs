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
            Guid id = Guid.NewGuid();
            Account account = new Account(id, input.LoginId, input.Password, input.Name, input.Email, input.Address);

            accountRepository.Save(account);

            return account.Id.ToString();
        }
    }
}
