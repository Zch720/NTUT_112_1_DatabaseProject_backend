﻿using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Elecookies.Controllers {
    [Route("api/user")]
    [ApiController]
    public class AccountController {
        private AccountRepository accountRepository;
        private StaffRepository staffRepository;

        public AccountController(AccountRepository accountRepository, StaffRepository staffRepository) {
            this.accountRepository = accountRepository;
            this.staffRepository = staffRepository;
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

        [Route ("create-staff")]
        [HttpPost]
        public string CreateStaff(CreateStaffInput input) {
            if (staffRepository.All().Find(staff => staff.LoginId == input.LoginId) != null) {
                return "";
            }
            if (staffRepository.All().Find(staff => staff.Email == input.Email) != null) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Staff staff = new Staff(id, Guid.Parse(input.ShopId), input.LoginId, input.Password, input.Name, input.Email, input.Address);
            
            staffRepository.Save(staff);

            return staff.Id.ToString();
        }

        [Route("delete-staff")]
        [HttpPost]
        public bool DeleteStaff(DeleteStaffInput input) {
            Staff? staff = staffRepository.FindById(Guid.Parse(input.Id));
            if (staff != null) {
                staffRepository.Delete(staff.Id);
                return true;
            }
            return false;
        }
    }
}
