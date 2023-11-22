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

        public override AccountController AccountController { get; }
        public override ShopController ShopController { get; }

        public InMemoryTestContext() {
            options = new DbContextOptionsBuilder<InMemoryDbContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            DbContext = new InMemoryDbContext(options);

            AccountRepository = new AccountRepository(DbContext);
            ShopRepository = new ShopRepository(DbContext);

            AccountController = new AccountController(AccountRepository);
            ShopController = new ShopController(ShopRepository);
        }
    }
}