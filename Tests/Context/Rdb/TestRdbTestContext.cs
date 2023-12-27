using Elecookies;
using Elecookies.Controllers;
using Elecookies.Repositories;
using Tests;
using Tests.Context.Rdb;

namespace Test.Context.Rdb {
    public class TestRdbTestContext : TestContext {
        public override ElecookiesDbContext DbContext { get; }

        public override AccountRepository AccountRepository { get; }
        public override ShopRepository ShopRepository { get; }
        public override StaffRepository StaffRepository { get; }

        public override AccountController AccountController { get; }
        public override ShopController ShopController { get; }

        public TestRdbTestContext() {
            DbContext = new TestRdbDbContext();

            AccountRepository = new AccountRepository(DbContext);
            ShopRepository = new ShopRepository(DbContext);
            StaffRepository = new StaffRepository(DbContext);

            AccountController = new AccountController(AccountRepository, StaffRepository);
            ShopController = new ShopController(ShopRepository);
        }
    }
}