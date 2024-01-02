using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Elecookies.Controllers {
    [Route("api/shop")]
    [ApiController]
    public class ShopController {
        private ShopRepository shopRepository;

        public ShopController(ShopRepository shopRepository) {
            this.shopRepository = shopRepository;
        }

        [Route("create")]
        [HttpPost]
        public string CreateShop(CreateShopInput input) {
            if (shopRepository.All().Find(shop => shop.Email == input.Email) != null) {
                return "";
            }
            Guid id = new Guid();
            Shop shop = new Shop(id, input.Name, input.Address, input.Email, input.PhoneNumber, input.Description);
            shopRepository.Save(shop);

            return shop.Id.ToString();
        }

        [Route("delete")]
        [HttpPost]
        public bool DeleteShop(DeleteShopInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.ShopId));
            if (shop != null) {
                shopRepository.Delete(shop.Id);
                return true;
            }
            return false;
        }

        [Route("edit-name")]
        [HttpPost]
        public void EditShopName(EditShopNameInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Name = input.Name;
                shopRepository.Save(shop);
            }
        }

        [Route("edit-address")]
        [HttpPost]
        public void EditShopAddress(EditShopAddressInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Address = input.Address;
                shopRepository.Save(shop);
            }
        }

        [Route("edit-email")]
        [HttpPost]
        public void EditShopEmail(EditShopEmailInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Email = input.Email;
                shopRepository.Save(shop);
            }
        }

        [Route("edit-phone-number")]
        [HttpPost]
        public void EditShopPhoneNumber(EditShopPhoneNumberInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.PhoneNumber = input.PhoneNumber;
                shopRepository.Save(shop);
            }
        }

        [Route("edit-description")]
        [HttpPost]
        public void EditShopDescription(EditShopDescriptionInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Description = input.Description;
                shopRepository.Save(shop);
            }
        }

        [Route("count/all")]
        [HttpGet]
        public int GetAllShopCount() {
            return shopRepository.All().Count;
        }

        [Route("list")]
        [HttpGet]
        public List<Shop> GetShopList(int from, int to) {
            List<Shop> shops = shopRepository.All();
            if (from <= to && from >= 0 && to < shops.Count) {
                return shops.GetRange(from, to - from + 1);
            }
            return new List<Shop>();
        }
    }
}
