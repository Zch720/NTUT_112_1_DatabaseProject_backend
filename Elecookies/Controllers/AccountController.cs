using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Elecookies.Controllers {
    [Route("api/user")]
    [ApiController]
    public class AccountController {
        private AccountRepository accountRepository;
        private StaffRepository staffRepository;
        private CustomerRepository customerRepository;
        private ShopRepository shopRepository;
        private CouponRepository couponRepository;

        public AccountController(AccountRepository accountRepository, StaffRepository staffRepository, CustomerRepository customerRepository, ShopRepository shopRepository, CouponRepository couponRepository) {
            this.accountRepository = accountRepository;
            this.staffRepository = staffRepository;
            this.customerRepository = customerRepository;
            this.shopRepository = shopRepository;
            this.couponRepository = couponRepository;
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
            if (account != null) {
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

        [Route("create-staff")]
        [HttpPost]
        public string CreateStaff(CreateStaffInput input) {
            if (staffRepository.All().Find(staff => staff.LoginId == input.LoginId) != null) {
                return "";
            }
            if (staffRepository.All().Find(staff => staff.Email == input.Email) != null) {
                return "";
            }
            if (shopRepository.FindById(Guid.Parse(input.ShopId)) == null) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Staff staff = new Staff(id, Guid.Parse(input.ShopId), input.LoginId, input.Password, input.Name, input.Email, input.Address);
            Shop shop = shopRepository.FindById(Guid.Parse(input.ShopId));
            staff.Shop = shop;
            shop.Staffs.Add(staff);
            staffRepository.Save(staff);
            shopRepository.Save(shop);

            return staff.Id.ToString();
        }

        [Route("delete-staff")]
        [HttpPost]
        public bool DeleteStaff(DeleteStaffInput input) {
            Staff? staff = staffRepository.FindById(Guid.Parse(input.Id));
            if (staff != null) {
                Shop shop = shopRepository.FindById(staff.ShopId);
                shop.Staffs.Remove(staff);
                staffRepository.Delete(staff.Id);
                shopRepository.Save(shop);
                return true;
            }
            return false;
        }

        [Route("signup")]
        [HttpPost]
        public string CreateCustomer(CreateCustomerInput input) {
            if (customerRepository.All().Find(customer => customer.LoginId == input.LoginId) != null) {
                return "";
            }
            if (customerRepository.All().Find(customer => customer.Email == input.Email) != null) {
                return "";
            }

            Guid id = Guid.NewGuid();
            Customer customer = new Customer(id, input.LoginId, input.Password, input.Name, input.Email, input.Address);

            customerRepository.Save(customer);

            return customer.Id.ToString();
        }

        [Route("delete-account")]
        [HttpPost]
        public bool DeleteCustomer(DeleteCustomerInput input) {
            Customer? customer = customerRepository.FindById(Guid.Parse(input.UserId));
            if (customer != null && customer.Password == input.Password) {
                customerRepository.Delete(customer.Id);
                return true;
            }
            return false;
        }

        [Route("follow-shop")]
        [HttpPost]
        public void FollowShop(FollowShopInput input) {
            Customer? customer = customerRepository.FindById(Guid.Parse(input.CustomerId));
            Shop? shop = shopRepository.FindById(Guid.Parse(input.ShopId));
            if (customer != null && shop != null) {
                customer.Shops.Add(shop);
                shop.Customers.Add(customer);
                customerRepository.Save(customer);
                shopRepository.Save(shop);
            }
        }

        [Route("add-coupon")]
        [HttpPost]
        public void AddCoupon(AddCouponInput input) {
            Customer? customer = customerRepository.FindById(Guid.Parse(input.CustomerId));
            Coupon? coupon = couponRepository.FindById(Guid.Parse(input.CouponId));
            if (customer != null && coupon != null) {
                Has? has = customerRepository.FindById(customer.Id, coupon.Id);
                if (has != null) {
                    has.Quantity += 1;
                } else {
                    has = new Has(customer.Id, coupon.Id, 1);
                    customer.Coupons.Add(coupon);
                    coupon.Customers.Add(customer);
                }
                customerRepository.Save(has);
            }
        }

        [Route("type")]
        [HttpGet]
        public string GetAccountType(string userId) {
            if (customerRepository.FindById(Guid.Parse(userId)) != null) {
                return "customer";
            }
            if (staffRepository.FindById(Guid.Parse(userId)) != null) {
                return "staff";
            }
            return "";
        }

        [Route("profile")]
        [HttpGet]
        public string GetAccountData(string userId) {
            if (customerRepository.FindById(Guid.Parse(userId)) != null) {
                return JsonSerializer.Serialize(customerRepository.FindById(Guid.Parse(userId)));
            }
            if (staffRepository.FindById(Guid.Parse(userId)) != null) {
                return JsonSerializer.Serialize(staffRepository.FindById(Guid.Parse(userId)));
            }
            
            return "";
        }

        [Route("followed-shops-count")]
        [HttpGet]
        public int GetFollowedShopCount(string userId) {
            Customer? customer = customerRepository.FindById(Guid.Parse(userId));
            if (customer != null) {
                return customer.Shops.Count;
            }
            return 0;
        }

        [Route("followed-shops")]
        [HttpGet]
        public List<Shop> GetFollowShops(string userId, int from, int to) {
            Customer? customer = customerRepository.FindById(Guid.Parse(userId));
            if (customer != null) {
                List<Shop> shops = customer.Shops;
                if (from <= to && from >= 0 && to < shops.Count) {
                    return shops.GetRange(from, to - from + 1);
                }
            }
            return new List<Shop>();
        }

        //public string GetOrders(string userId, int from, int to) {
        //    Customer? customer = customerRepository.FindById(Guid.Parse(userId));
        //    if (customer != null) {
        //        List<ShopOrder> shopOrders = customer.ShopOrders;
        //        if (from <= to && from >= 0 && to < shopOrders.Count) {
        //            List<ShopOrder> resultOrders = shopOrders.GetRange(from, to - from + 1);
        //        }
        //    }
        //    return "";
        //}
    }
}