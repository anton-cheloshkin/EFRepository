using Microsoft.EntityFrameworkCore;

namespace Example.Persistance.Providers.Memory
{
    public class TestContextMemory : Context.TestContext
    {
        public TestContextMemory(DbContextOptions<TestContextMemory> op) : base(op) { }
    }
}
