using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;
using Elecookies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Elecookies.Controllers {
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController {
        private ShoppingCartRepository shoppingCartRepository;
        private CustomerRepository customerRepository;
        private ProductRepository productRepository;
        private ShopRepository shopRepository;

        public ShoppingCartController(ShoppingCartRepository shoppingCartRepository, CustomerRepository customerRepository, ProductRepository productRepository, ShopRepository shopRepository) {
            this.shoppingCartRepository = shoppingCartRepository;
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.shopRepository = shopRepository;
        }

        [Route("create")]
        [HttpPost]
        public string CreateShoppingCart(CreateShoppingCartInput input) {
            if (shoppingCartRepository.FindById(Guid.Parse(input.CustomerId)) != null) {
                return "";
            }
            if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
                return "";
            }

            ShoppingCart shoppingCart = new ShoppingCart(Guid.Parse(input.CustomerId));
            Customer customer = customerRepository.FindById(Guid.Parse(input.CustomerId))!;
            customer.ShoppingCart = shoppingCart;
            shoppingCart.Customer = customer;

            shoppingCartRepository.Save(shoppingCart);
            customerRepository.Save(customer);

            return shoppingCart.CustomerId.ToString();
        }

        [Route("set-quantity")]
        [HttpPost]
        public void SetQuantityToShoppingCart(SetQuantityToShoppingCartInput input) {
            // TODO: Did not save into database correctlly.
            //if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
            //    return;
            //}
            //if (productRepository.FindById(Guid.Parse(input.ProductId)) == null ) {
            //    return;
            //}

            //ShoppingCart? shoppingCart = shoppingCartRepository.FindById(Guid.Parse(input.CustomerId));
            //if (shoppingCart == null) return;
            //foreach (ShoppingCartHas product in shoppingCart.ShoppingCartHas) {
            //    if (product.ProductId == Guid.Parse(input.ProductId)) {
            //        product.Quantity = input.Number;
            //        shoppingCartRepository.Save(shoppingCart);
            //        return;
            //    }
            //}
            //ShoppingCartHas has = new(Guid.Parse(input.CustomerId), Guid.Parse(input.ProductId), input.Number);
            //shoppingCart.ShoppingCartHas.Add(has);
            //shoppingCartRepository.Save(shoppingCart);
        }

        [Route("remove")]
        [HttpPost]
        public bool RemoveFromShoppingCart(RemoveFromShoppingCartInput input) {
            if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
                return false;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId)) == null) {
                return false;
            }

            ShoppingCartHas? shoppingCartHas = shoppingCartRepository.FindById(Guid.Parse(input.CustomerId), Guid.Parse(input.ProductId));
            if (shoppingCartHas != null) {
                shoppingCartRepository.Delete(Guid.Parse(input.CustomerId), Guid.Parse(input.ProductId));
                return true;
            }
            return false;
        }

        [Route("delete")]
        [HttpPost]
        public bool DeleteShoppingCart(DeleteShoppingCartInput input) {
            ShoppingCart? shoppingCart = shoppingCartRepository.FindById(Guid.Parse(input.CustomerId));
            if (shoppingCart != null) {
                Customer customer = customerRepository.FindById(shoppingCart.CustomerId)!;
                customer.ShoppingCart = null;
                customerRepository.Save(customer);
                shoppingCartRepository.Delete(shoppingCart.CustomerId);
                return true;
            }
            return false;
        }

        [Route("get")]
        [HttpGet]
        public ShoppingCartDataOutput? GetShoppingCart(string userId) {
            ShoppingCart? shoppingCart = shoppingCartRepository.FindById(Guid.Parse(userId));
            if (shoppingCart == null) return null;

            ShoppingCartDataOutput output = new();
            Dictionary<Guid, List<ShoppingCartProductDataOutput>> shops = new();
            foreach (ShoppingCartHas product in shoppingCart.ShoppingCartHas) {
                if (!shops.ContainsKey(product.Product.ShopId))
                    shops.Add(product.Product.ShopId, new());
                ShoppingCartProductDataOutput productOutput = new();
                productOutput.Id = product.ProductId.ToString();
                productOutput.Name = product.Product.Name;
                productOutput.Price = product.Product.Price;
                productOutput.Quantity = product.Quantity;
                productOutput.Stock = product.Product.Stock;
                productOutput.Image = product.Product.Images.Count != 0 ? product.Product.Images.ElementAt(0).Image : "";
                shops[product.Product.ShopId].Add(productOutput);
            }
            foreach (KeyValuePair<Guid, List<ShoppingCartProductDataOutput>> shop in shops) {
                Shop s = shopRepository.FindById(shop.Key)!;
                ShoppingCartShopDataOutput shopOutput = new();
                shopOutput.ShopId = s.Id.ToString();
                shopOutput.ShopName = s.Name;
                shopOutput.ShopIcon = s.Icon;
                shopOutput.Products = shop.Value;
                output.Shops.Add(shopOutput);
            }
            return output;
        }
    }
}
