using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Elecookies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

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

        [Route("all-shop-count")]
        [HttpGet]
        public int GetAllShopCount() {
            return shopRepository.All().Count;
        }

        [Route("list")]
        [HttpGet]
        public List<ShopListItemOutput> GetShopList(int from, int to) {
            List<Shop> shops = shopRepository.All();
            return shops.GetRange(from, to - from + 1)
                .ConvertAll(shop => {
                    ShopListItemOutput output = new();
                    output.Id = shop.Id.ToString();
                    output.Name = shop.Name;
                    output.Icon = shop.Icon;
                    return output;
                });
        }

        [Route("data")]
        [HttpGet]
        public ShopDataOutput GetShopData(string shopId) {
            ShopDataOutput output = new();
            Shop? shop = shopRepository.FindById(Guid.Parse(shopId));
            if (shop != null) {
                output.Id = shop.Id.ToString();
                output.Icon = "";
                output.Name = shop.Name;
                output.Address = shop.Address;
                output.Description = shop.Description;
                output.Email = shop.Email;
                output.PhoneNumber = shop.PhoneNumber;
            }
            return output;
        }

        [Route("product/count")]
        [HttpGet]
        public int GetShopProductCount(string shopId, string productType) {
            try {
                return shopRepository
                    .GetProducts(Guid.Parse(shopId))
                    .Count(product => product.Category.Contains(GetProductCategory(productType)));
            } catch {
                return 0;
            }
        }

        [Route("products")]
        [HttpGet]
        public List<ProductListItemOutput> GetProducts(string shopId, string productType, int from, int to) {
            try {
                return shopRepository
                    .GetProducts(Guid.Parse(shopId))
                    .FindAll(product => product.Category.Contains(GetProductCategory(productType)))
                    .GetRange(from, to - from + 1)
                    .ConvertAll(product => {
                        ProductListItemOutput output = new ProductListItemOutput();
                        output.Id = product.Id.ToString();
                        output.Name = product.Name;
                        output.Price = product.Price;
                        output.Image = product.Images.Count != 0 ? product.Images.ElementAt(0).Image : "";
                        return output;
                    });
            } catch {
                return new();
            }
        }

        private string GetProductCategory(string engCategoryName) {
            if (engCategoryName == "all") {
                return "";
            } else if (engCategoryName == "chocolate-cookie") {
                return "巧克力餅乾";
            } else if (engCategoryName == "butter-cookie") {
                return "奶油餅乾";
            } else if (engCategoryName == "sandwitch-cookie") {
                return "夾心餅乾";
            } else if (engCategoryName == "cookies") {
                return "曲奇餅乾";
            } else if (engCategoryName == "soft-cookie") {
                return "美式軟餅乾";
            } else if (engCategoryName == "roll-puff-pastry") {
                return "捲心酥";
            } else if (engCategoryName == "egg-roll") {
                return "蛋捲";
            } else if (engCategoryName == "other") {
                return "其他";
            }
            throw new Exception("Category name is illegal");
        }
    }
}
