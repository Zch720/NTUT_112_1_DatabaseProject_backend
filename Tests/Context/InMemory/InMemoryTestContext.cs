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

        public override AccountController AccountController { get; }

        public InMemoryTestContext() {
            options = new DbContextOptionsBuilder<InMemoryDbContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            DbContext = new InMemoryDbContext(options);

            AccountRepository = new AccountRepository(DbContext);
            AccountController = new AccountController(AccountRepository);
        }
    }
}