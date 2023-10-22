using Elecookies;
using Tests;
using Tests.Context.Rdb;

namespace Test.Context.Rdb {
    public class TestRdbTestContext : TestContext {
        public override ElecookiesDbContext DbContext { get; }

        public TestRdbTestContext() {
            DbContext = new TestRdbDbContext();
        }
    }
}