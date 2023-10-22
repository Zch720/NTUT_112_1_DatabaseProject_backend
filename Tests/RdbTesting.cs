using Test.Context.Rdb;

namespace Tests {
    public class RdbTesting {
        private static TestRdbTestContext context { get; }

        static RdbTesting() {
            context = new TestRdbTestContext();
        }
    }
}