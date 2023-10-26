using Elecookies;
using Elecookies.Controllers;
using Elecookies.Repositories;
using Tests;
using Tests.Context.Rdb;

namespace Test.Context.Rdb {
    public class TestRdbTestContext : TestContext {
        public override ElecookiesDbContext DbContext { get; }

        public override AccountRepository AccountRepository { get; }

        public override AccountController AccountController { get; }

        public TestRdbTestContext() {
            DbContext = new TestRdbDbContext();

            AccountRepository = new AccountRepository(DbContext);
            AccountController = new AccountController(AccountRepository);
        }
    }
}