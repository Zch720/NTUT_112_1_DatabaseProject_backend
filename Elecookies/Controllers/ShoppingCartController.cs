using Elecookies.Entities;
using Elecookies.ReadModels;
using Elecookies.Repositories;

namespace Elecookies.Controllers {
    public class ShoppingCartController {
        private ShoppingCartRepository shoppingCartRepository;
        private CustomerRepository customerRepository;
        private ProductRepository productRepository;

        public ShoppingCartController(ShoppingCartRepository shoppingCartRepository, CustomerRepository customerRepository, ProductRepository productRepository) {
            this.shoppingCartRepository = shoppingCartRepository;
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
        }

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

        public void SetQuantityToShoppingCart(SetQuantityToShoppingCartInput input) {
            if (customerRepository.FindById(Guid.Parse(input.CustomerId)) == null) {
                return;
            }
            if (productRepository.FindById(Guid.Parse(input.ProductId)) == null ) {
                return;
            }

            ShoppingCartHas? shoppingCartHas = shoppingCartRepository.FindById(Guid.Parse(input.CustomerId), Guid.Parse(input.ProductId));
            if (shoppingCartHas != null) {
                shoppingCartHas.Quantity = input.Number;
            }
            else {
                shoppingCartHas = new ShoppingCartHas(Guid.Parse(input.CustomerId), Guid.Parse(input.ProductId), input.Number);
            }

            shoppingCartRepository.Save(shoppingCartHas);
        }
        
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
    }
}
