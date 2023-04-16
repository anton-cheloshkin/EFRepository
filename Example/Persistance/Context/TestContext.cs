using EFRepository;
using EFRepository.Builders;
using Microsoft.EntityFrameworkCore;
using Example.Persistance.Models;
using Example.Persistance.Repository;

namespace Example.Persistance.Context
{
    public abstract class TestContext : DbContext, IDbContextTest
    {
        public TestContext(DbContextOptions op) : base(op) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<TwoPKEntity>()
                .HasKey(x => new { x.Id, x.Second });

        }
        public DbSet<OnePKEntity> OnePKEntity { get; set; }
        public DbSet<TwoPKEntity> TwoPKEntity { get; set; }
    }
    public interface IDbContextTest : IEFContext, IDisposable
    {
        DbSet<OnePKEntity> OnePKEntity { get; set; }
        DbSet<TwoPKEntity> TwoPKEntity { get; set; }
    }
    public interface ITestProvider : IEFDataProvider { }
    public class TestDataProvider : EFDataProvider, ITestProvider
    {
        public TestDataProvider(IDbContextTest ctx) : base((TestContext)ctx)
        {
        }
    }
    public static class TestRepositoryBuilder
    {
        public static void AddTestRepository<TContext, TContextImpl, TProvider, TProvoderImpl>(this EFContextBuilder<TContext, TContextImpl, TProvider, TProvoderImpl> builder)
            where TContext : IDbContextTest
            where TContextImpl : TestContext, TContext
            where TProvider : IEFDataProvider
            where TProvoderImpl : TestDataProvider
        {
            builder
                .AddRepository<OnePKEntity, OnePKRepository>()
                .AddRepository<TwoPKEntity, TwoPKRepository>();
        }
    }
}
