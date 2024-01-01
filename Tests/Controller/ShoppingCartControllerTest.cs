using Elecookies.ReadModels;
using Elecookies.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controller {
    public class ShoppingCartControllerTest {
        private TestContext context { get; }

        public ShoppingCartControllerTest(TestContext testContext) {
            this.context = testContext;
        }

        [TestCleanup]
        public void TearDown() {
            var shoppingCarts = context.ShoppingCartRepository.All();
            var accounts = context.AccountRepository.All();
            var products = context.ProductRepository.All();
            var customers = context.CustomerRepository.All();
            var shops = context.ShopRepository.All();
            foreach (var shoppingCart in shoppingCarts) {
                context.ShoppingCartRepository.Delete(shoppingCart.CustomerId);
            }
            foreach (var account in accounts) {
                context.AccountRepository.Delete(account.Id);
            }
            foreach (var product in  products) {
                context.ProductRepository.Delete(product.Id);
            }
            foreach (var customer in customers) {
                context.CustomerRepository.Delete(customer.Id);
            }
            foreach (var shop in shops) {
                context.ShopRepository.Delete(shop.Id);
            }
        }

        [TestMethod]
        public void CreateShoppingCart() {
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            CreateShoppingCartInput input = new CreateShoppingCartInput();
            input.CustomerId = customerId;

            string id = context.ShoppingCartController.CreateShoppingCart(input);

            Assert.IsNotNull(context.ShoppingCartRepository.FindById(Guid.Parse(id)));
        }

        [TestMethod]
        public void SetQuantityToShoppingCart() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            string shoppingCartId = context.CreateNewShoppingCart(customerId);
            SetQuantityToShoppingCartInput input = new SetQuantityToShoppingCartInput();
            input.CustomerId = customerId;
            input.ProductId = productId;
            input.Number = 3;

            context.ShoppingCartController.SetQuantityToShoppingCart(input);

            Assert.AreEqual(3, context.ShoppingCartRepository.FindById(Guid.Parse(customerId), Guid.Parse(productId))!.Quantity);
        }

        [TestMethod]
        public void RemoveFromShoppingCart() {
            string accountId = context.CreateNewStaff("loginId", "password", "email");
            string shopId = context.StaffRepository.FindById(Guid.Parse(accountId))!.ShopId.ToString();
            string productId = context.CreateNewProduct(accountId, shopId);
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            context.AddNewToShoppingCart(customerId, productId);
            RemoveFromShoppingCartInput input = new RemoveFromShoppingCartInput();
            input.CustomerId = customerId;
            input.ProductId = productId;

            Assert.IsNotNull(context.ShoppingCartRepository.FindById(Guid.Parse(customerId), Guid.Parse(productId)));
            bool success = context.ShoppingCartController.RemoveFromShoppingCart(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.ShoppingCartRepository.FindById(Guid.Parse(customerId), Guid.Parse(productId)));
        }

        [TestMethod]
        public void DeleteShoppingCart() {
            string customerId = context.CreateNewCustomer("LoginId", "Password", "Email");
            string shoppingCartId = context.CreateNewShoppingCart(customerId);
            DeleteShoppingCartInput input = new DeleteShoppingCartInput();
            input.CustomerId = customerId;

            Assert.IsNotNull(context.ShoppingCartRepository.FindById(Guid.Parse(customerId)));
            bool success = context.ShoppingCartController.DeleteShoppingCart(input);

            Assert.IsTrue(success);
            Assert.IsNull(context.ShoppingCartRepository.FindById(Guid.Parse(customerId)));
        }
    }
}
