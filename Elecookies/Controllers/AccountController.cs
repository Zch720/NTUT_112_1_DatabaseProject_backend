using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Elecookies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        public string CreateCustomer(CreateAccountInput input) {
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

        [Route("modify/name")]
        [HttpPut]
        public void EditAccountName(EditAccountNameInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Name = input.Name;
                accountRepository.Save(account);
            }
        }

        [Route("modify/password")]
        [HttpPut]
        public void EditAccountPassword(EditAccountPasswordInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null && account.Password == input.Password) {
                account.Password = input.NewPassword;
                accountRepository.Save(account);
            }
        }

        [Route("modify/email")]
        [HttpPut]
        public void EditAccountEmail(EditAccountEmailInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Email = input.Email;
                accountRepository.Save(account);
            }
        }

        [Route("modify/address")]
        [HttpPut]
        public void EditAccountAddress(EditAccountAddressInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Address = input.Address;
                accountRepository.Save(account);
            }
        }

        [Route("modify/phone")]
        [HttpPut]
        public void EditAccountPhone(EditAccountPhoneInput input) {
            Account? account = accountRepository.FindById(Guid.Parse(input.UserId));
            if (account != null) {
                account.Phone = input.Phone;
                accountRepository.Save(account);
            }
        }

        [Route("signin")]
        [HttpPost]
        public string Login(LoginInput input) {
            Account? account = null;
            
            if (account == null )
                account = customerRepository.All().Find(account => {
                    return account.LoginId == input.UserAccount && account.Password == input.Password;
                });
            if (account == null)
                account = customerRepository.All().Find(account => {
                    return account.Email == input.UserAccount && account.Password == input.Password;
                });

            if (account == null)
                account = staffRepository.All().Find(account => {
                    return account.LoginId == input.UserAccount && account.Password == input.Password;
                });
            if (account == null)
                account = staffRepository.All().Find(account => {
                    return account.Email == input.UserAccount && account.Password == input.Password;
                });


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
            //Customer? customer = customerRepository.FindById(Guid.Parse(input.CustomerId));
            //Shop? shop = shopRepository.FindById(Guid.Parse(input.ShopId));
            //if (customer == null || shop == null) return;

            if (input.Follow) {
                //customer.Shops.Add(shop);
                //shop.Customers.Add(customer);
                //customerRepository.Save(customer);
                //shopRepository.Save(shop);
                customerRepository.FollowShop(Guid.Parse(input.CustomerId), Guid.Parse(input.ShopId));
            } else {
                //customer.Shops.Remove(customer.Shops.First(s => s.Id == shop.Id));
                //shop.Customers.Remove(shop.Customers.First(c => c.Id == customer.Id));
                //customerRepository.Save(customer);
                //shopRepository.Save(shop);
                customerRepository.UnfollowShop(Guid.Parse(input.CustomerId), Guid.Parse(input.ShopId));
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
            Account? account = null;
            if (account == null) {
                account = customerRepository.FindById(Guid.Parse(userId));
            }
            if (account == null) {
                account = staffRepository.FindById(Guid.Parse(userId));
            }
            
            if (account != null) {
                AccountProfileOutput output = new AccountProfileOutput();
                output.LoginId = account.LoginId;
                output.Name = account.Name;
                output.Address = account.Address;
                output.Email = account.Email;
                output.Phone = account.Phone;
                return JsonSerializer.Serialize(output);
            }
            return "";
        }

        [Route("followed-shop")]
        [HttpGet]
        public bool GetIsShopFollowed(string userId, string shopId) {
            return customerRepository
                .GetCustomerFollows(Guid.Parse(userId))
                .Find(shop => shop.Id == Guid.Parse(shopId)) != null;
        }

        [Route("followed-shops-count")]
        [HttpGet]
        public int GetFollowedShopCount(string userId) {
            return customerRepository.GetCustomerFollows(Guid.Parse(userId)).Count;
        }

        [Route("followed-shops")]
        [HttpGet]
        public List<CustomerFollowedShopsOutput> GetFollowShops(string userId, int from, int to) {
            List<CustomerFollowedShopsOutput> shops = new List<CustomerFollowedShopsOutput>();
            
            List<Shop> follows = customerRepository.GetCustomerFollows(Guid.Parse(userId));
            foreach (Shop shop in follows.GetRange(from, to - from + 1)) {
                CustomerFollowedShopsOutput output = new();
                output.Id = shop.Id.ToString();
                output.Name = shop.Name;
                output.Icon = shop.Icon;
                shops.Add(output);
            }
            return shops;
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

        [Route("staff/get-shop-profile")]
        [HttpGet]
        public ShopDataOutput? StaffGetShopProfile(string staffId) {
            Staff? staff = staffRepository.FindById(Guid.Parse(staffId));
            if (staff == null) return null;
            ShopDataOutput output = new();
            output.Id = staff.Shop.Id.ToString();
            output.Name = staff.Shop.Name;
            output.Address = staff.Shop.Address;
            output.PhoneNumber = staff.Shop.PhoneNumber;
            output.Icon = staff.Shop.Icon;
            output.Description = staff.Shop.Description;
            output.Email = staff.Shop.Email;
            return output;
        }

        [Route("staff/shop/product/count")]
        [HttpGet]
        public int StaffGetShopProductsCount(string userId, string productType) {
            Staff? staff = staffRepository.FindById(Guid.Parse(userId));
            if (staff == null) return 0;
            try {
                return shopRepository
                    .GetProducts(staff.ShopId)
                    .Count(product => product.Category.Contains(GetProductCategory(productType)));
            } catch {
                return 0;
            }
        }

        [Route("staff/shop/products")]
        [HttpGet]
        public List<ProductListItemOutput> StaffGetShopProducts(string userId, string productType, int from, int to) {
            Staff? staff = staffRepository.FindById(Guid.Parse(userId));
            if (staff == null) return new();
            try {
                return shopRepository
                    .GetProducts(staff.ShopId)
                    .FindAll(product => product.Category.Contains(GetProductCategory(productType)))
                    .GetRange(from, to - from + 1)
                    .ConvertAll(product => {
                        ProductListItemOutput output = new ProductListItemOutput();
                        output.Id = product.Id.ToString();
                        output.Name = product.Name;
                        output.Price = product.Price;
                        output.Image = product.Images.Count != 0 ? product.Images.ElementAt(0).Image : "";
                        output.Stock = product.Stock;
                        return output;
                    });
            }
            catch {
                return new();
            }
        }

        private string GetProductCategory(string engCategoryName) {
            if (engCategoryName == "all") {
                return "";
            }
            else if (engCategoryName == "chocolate-cookie") {
                return "巧克力餅乾";
            }
            else if (engCategoryName == "butter-cookie") {
                return "奶油餅乾";
            }
            else if (engCategoryName == "sandwitch-cookie") {
                return "夾心餅乾";
            }
            else if (engCategoryName == "cookies") {
                return "曲奇餅乾";
            }
            else if (engCategoryName == "soft-cookie") {
                return "美式軟餅乾";
            }
            else if (engCategoryName == "roll-puff-pastry") {
                return "捲心酥";
            }
            else if (engCategoryName == "egg-roll") {
                return "蛋捲";
            }
            else if (engCategoryName == "other") {
                return "其他";
            }
            throw new Exception("Category name is illegal");
        }
    }
}