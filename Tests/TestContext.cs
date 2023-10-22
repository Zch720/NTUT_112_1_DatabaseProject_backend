using Elecookies;

namespace Tests {
    public abstract class TestContext {
        public abstract ElecookiesDbContext DbContext { get; }
    }
}