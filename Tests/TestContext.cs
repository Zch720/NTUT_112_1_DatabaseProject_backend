using Elecookies;
using Elecookies.Controllers;
using Elecookies.Repositories;

namespace Tests {
    public abstract class TestContext {
        public abstract ElecookiesDbContext DbContext { get; }

        public abstract AccountRepository AccountRepository { get; }

        public abstract AccountController AccountController { get; }
    }
}