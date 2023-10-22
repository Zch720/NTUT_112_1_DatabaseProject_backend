using Test.Context.InMemory;

namespace Tests {
    public class InMemoryTesting {
        private static InMemoryTestContext context { get; }

        static InMemoryTesting() {
            context = new InMemoryTestContext();
        } 
    }
}