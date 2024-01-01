using Elecookies;
using Elecookies.Controllers;
using Elecookies.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tests;
using Tests.Context.InMemory;

namespace Test.Context.InMemory {
    public class InMemoryTestContext : TestContext {
        private DbContextOptions<InMemoryDbContext> options { get; }

        public override ElecookiesDbContext DbContext { get; }

        public override AccountRepository AccountRepository { get; }
        public override ShopRepository ShopRepository { get; }
        public override StaffRepository StaffRepository { get; }
        public override ProductRepository ProductRepository { get; }
        public override CustomerRepository CustomerRepository { get; }
        public override ShoppingCartRepository ShoppingCartRepository { get; }
        public override CouponRepository CouponRepository { get; }
        public override ShopOrderRepository ShopOrderRepository { get; }

        public override AccountController AccountController { get; }
        public override ShopController ShopController { get; }
        public override ProductController ProductController { get; }
        public override ShoppingCartController ShoppingCartController { get; }
        public override CouponController CouponController { get; }
        public override ShopOrderController ShopOrderController { get; }

        public InMemoryTestContext() {
            options = new DbContextOptionsBuilder<InMemoryDbContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            DbContext = new InMemoryDbContext(options);

            AccountRepository = new AccountRepository(DbContext);
            ShopRepository = new ShopRepository(DbContext);
            StaffRepository = new StaffRepository(DbContext);
            ProductRepository = new ProductRepository(DbContext);
            CustomerRepository = new CustomerRepository(DbContext);
            ShoppingCartRepository = new ShoppingCartRepository(DbContext);
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