using EFRepository;
using EFRepository.Builders;
using Example.Persistance.Context;
using Example.Persistance.Models;
using Example.Persistance.Providers.Memory;
using Example.Persistance.Providers.PgSQL;
using Example.Persistance.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class Test
    {
        IServiceScopeFactory Factory { get; set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            // in-memory implementation

            services
                .AddEFContext<IDbContextTest, TestContextMemory, ITestProvider, TestDataProvider>()
                .AddTestRepository();

            //or
            services
                .AddEFContext<IDbContextTest, TestContextMemory, ITestProvider, TestDataProvider>()
                .AddRepository<OnePKEntity, OnePKRepository>()
                .AddRepository<TwoPKEntity, TwoPKRepository>();

            Factory = services
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();

            // or npgsql implementation

            services
                .AddEFContext<IDbContextTest, TestContextPgSQL, ITestProvider, TestDataProvider>()
                .AddTestRepository();

            Factory = services
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();
        }

        [Test]
        public void Test1()
        {
            using var scope = Factory.CreateScope();
            var sp = scope.ServiceProvider;

            var oneRepo = sp.GetRequiredService<IEFRepository<OnePKEntity>>();
            var twoRepo = sp.GetRequiredService<IEFRepository<TwoPKEntity>>();

            Assert.That(oneRepo, Is.Not.Null);
            Assert.That(twoRepo, Is.Not.Null);

            Assert.Pass();
        }
    }
}