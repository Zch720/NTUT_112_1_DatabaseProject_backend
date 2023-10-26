using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Context.Rdb;
using Tests.Controller;

namespace Tests {
    public class RdbTesting {
        private static TestRdbTestContext context { get; }

        static RdbTesting() {
            context = new TestRdbTestContext();
        }

        [TestClass]
        public class AccountControllerTesting : AccountControllerTest {
            public AccountControllerTesting() : base(context) {
            }
        }
    }
}