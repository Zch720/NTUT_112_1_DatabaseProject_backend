using Elecookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tests;
using Tests.Context.InMemory;

namespace Test.Context.InMemory {
    public class InMemoryTestContext : TestContext {
        private DbContextOptions<InMemoryDbContext> options { get; }

        public override ElecookiesDbContext DbContext { get; }

        public InMemoryTestContext() {
            options = new DbContextOptionsBuilder<InMemoryDbContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            DbContext = new InMemoryDbContext(options);
        }
    }
}