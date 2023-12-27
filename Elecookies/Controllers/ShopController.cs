using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;

namespace Elecookies.Controllers {
    public class ShopController {
        private ShopRepository shopRepository;

        public ShopController(ShopRepository shopRepository) {
            this.shopRepository = shopRepository;
        }

        public string CreateShop(CreateShopInput input) {
            Guid id = new Guid();
            Shop shop = new Shop(id, input.Name, input.Address, input.Email, input.PhoneNumber, input.Description);
            shopRepository.Save(shop);

            return shop.Id.ToString();
        }

        public bool DeleteShop(DeleteShopInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.ShopId));
            if (shop != null) {
                shopRepository.Delete(shop.Id);
                return true;
            }
            return false;
        }

        public void EditShopName(EditShopNameInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Name = input.Name;
                shopRepository.Save(shop);
            }
        }

        public void EditShopAddress(EditShopAddressInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Address = input.Address;
                shopRepository.Save(shop);
            }
        }

        public void EditShopEmail(EditShopEmailInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Email = input.Email;
                shopRepository.Save(shop);
            }
        }

        public void EditShopPhoneNumber(EditShopPhoneNumberInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.PhoneNumber = input.PhoneNumber;
                shopRepository.Save(shop);
            }
        }

        public void EditShopDescription(EditShopDescriptionInput input) {
            Shop? shop = shopRepository.FindById(Guid.Parse(input.Id));
            if (shop != null) {
                shop.Description = input.Description;
                shopRepository.Save(shop);
            }
        }
    }
}
