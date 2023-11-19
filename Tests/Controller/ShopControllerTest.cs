using Elecookies.ReadModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Controller {
    public class ShopControllerTest {
        private TestContext context { get; }

        public ShopControllerTest(TestContext testContext) {
            this.context = testContext;
        }

        [TestCleanup]
        public void TearDown() {
            var shops = context.ShopRepository.All();
            foreach (var shop in shops) {
                context.ShopRepository.Delete(shop.Id);
            }
        }

        [TestMethod]
        public void CreateShop() {
            CreateShopInput input = new CreateShopInput();
            input.Name = "shopName";
            input.Address = "address";
            input.Email = "email";
            input.PhoneNumber = "phoneNumber";
            input.Description = "description";

            string id = context.ShopController.CreateShop(input);

            Assert.IsNotNull(context.ShopRepository.FindById(Guid.Parse(id)));
        }
    }
}
