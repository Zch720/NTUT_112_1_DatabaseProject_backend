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
    }
}
