using Elecookies;
using Elecookies.Controllers;
using Elecookies.Entities;
using Elecookies.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests;
using Tests.Context.Rdb;

namespace Test.Context.Rdb {
    public class TestRdbTestContext : TestContext {
        public override ElecookiesDbContext DbContext { get; }

        public override AccountRepository AccountRepository { get; }
        public override ShopRepository ShopRepository { get; }
        public override StaffRepository StaffRepository { get; }
        public override ProductRepository ProductRepository { get; }
        public override ShoppingCartRepository ShoppingCartRepository { get; }
        public override CustomerRepository CustomerRepository { get; }
        public override CouponRepository CouponRepository { get; }
        public override ShopOrderRepository ShopOrderRepository { get; }

        public override AccountController AccountController { get; }
        public override ShopController ShopController { get; }
        public override ProductController ProductController { get; }
        public override ShoppingCartController ShoppingCartController { get; }
        public override CouponController CouponController { get; }
        public override ShopOrderController ShopOrderController { get; }

        public TestRdbTestContext() {
            DbContext = new TestRdbDbContext();

            AccountRepository = new AccountRepository(DbContext);
            ShopRepository = new ShopRepository(DbContext);
            StaffRepository = new StaffRepository(DbContext);
            ProductRepository = new ProductRepository(DbContext);
            ShoppingCartRepository = new ShoppingCartRepository(DbContext);
            CustomerRepository = new CustomerRepository(DbContext);
            CouponRepository = new CouponRepository(DbContext);
            ShopOrderRepository = new ShopOrderRepository(DbContext);

            AccountController = new AccountController(AccountRepository, StaffRepository, CustomerRepository, ShopRepository, CouponRepository);
            ShopController = new ShopController(ShopRepository);
            ProductController = new ProductController(ProductRepository, StaffRepository, ShopRepository);
            ShoppingCartController = new ShoppingCartController(ShoppingCartRepository, CustomerRepository, ProductRepository);
            CouponController = new CouponController(CouponRepository, StaffRepository, ShopRepository);
            ShopOrderController = new ShopOrderController(ShopOrderRepository, CustomerRepository, ProductRepository, ShopRepository, CouponRepository);
        }
    }
}