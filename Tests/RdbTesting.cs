using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Context.Rdb;
using Tests.Controller;

namespace Tests {
    public class RdbTesting {
        private static TestRdbTestContext context { get; }

        static RdbTesting() {
            context = new TestRdbTestContext();
        }

        [TestClass]
        public class AccountControllerTesting : AccountControllerTest {
            public AccountControllerTesting() : base(context) {
            }
        }

        [TestClass]
        public class ShopControllerTesting : ShopControllerTest {
            public ShopControllerTesting() : base(context) {
            }
        }

        [TestClass]
        public class ProductControllerTesting : ProductControllerTest {
            public ProductControllerTesting() : base(context) {
            }
        }

        [TestClass]
        public class ShoppingCartControllerTesting : ShoppingCartControllerTest {
            public ShoppingCartControllerTesting() : base(context) {
            }
        }

        [TestClass]
        public class CouponControllerTesting : CouponControllerTest {
            public CouponControllerTesting() : base(context) {
            }
        }

        [TestClass]
        public class ShopOrderControllerTesting : ShopOrderControllerTest {
            public ShopOrderControllerTesting() : base(context) {
            }
        }
    }
}