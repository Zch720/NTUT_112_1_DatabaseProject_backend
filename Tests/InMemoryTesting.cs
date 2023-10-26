using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Context.InMemory;
using Tests.Controller;

namespace Tests {
    public class InMemoryTesting {
        private static InMemoryTestContext context { get; }

        static InMemoryTesting() {
            context = new InMemoryTestContext();
        }

        [TestClass]
        public class AccountControllerTesting : AccountControllerTest {
            public AccountControllerTesting() : base(context) {
            }
        }
    }
}