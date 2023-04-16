using Microsoft.EntityFrameworkCore;
using Example.Persistance.Models;

namespace Example.Persistance.Providers.PgSQL
{
    public class TestContextPgSQL : Context.TestContext
    {
        public TestContextPgSQL(DbContextOptions<TestContextPgSQL> op) : base(op) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder
            //    .Entity<OnePKEntity>()
            //    .Property(x => x.Data)
            //    .HasColumnType("jsonb");
           
            modelBuilder
                .Entity<TwoPKEntity>()
                .Property(x => x.Data)
                .HasColumnType("jsonb[]");
        }
    }
}
