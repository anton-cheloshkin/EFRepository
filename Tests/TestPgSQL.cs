using EFRepository;
using EFRepository.Builders;
using Example.Persistance.Context;
using Example.Persistance.Models;
using Example.Persistance.Providers.Memory;
using Example.Persistance.Providers.PgSQL;
using Example.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Tests
{
    public class TestPgSQL
    {
        IServiceScopeFactory Factory { get; set; }
        public IConfiguration Configuration { get; init; }
        public TestPgSQL()
        {
            Configuration = new ConfigurationBuilder().AddUserSecrets(GetType().Assembly).Build();
        }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services
                .AddEFContext<IDbContextTest, TestContextPgSQL, ITestProvider, TestDataProvider>((sp, o) =>
                {
                    var s = Configuration.GetConnectionString("pgsql");
                    o.UseNpgsql(s);
                })
                .AddTestRepository();

            Factory = services
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();
        }

        [Test]
        public async Task OnePK()
        {
            using var scope = Factory.CreateScope();
            var sp = scope.ServiceProvider;

            var oneRepo = sp.GetRequiredService<IEFRepository<OnePKEntity>>();
            var twoRepo = sp.GetRequiredService<IEFRepository<TwoPKEntity>>();

            Assert.That(oneRepo, Is.Not.Null);
            Assert.That(twoRepo, Is.Not.Null);

            await oneRepo.FirstOrDefaultAsync(r => r.Id == 1);
            await oneRepo.WithPK(1).FirstOrDefaultAsync();
            await oneRepo.WithPK(2).FirstOrDefaultAsync();
            await oneRepo.WithPK(3).FirstOrDefaultAsync();
            await oneRepo.WithPK(4).FirstOrDefaultAsync();
            
            await oneRepo.FirstOrDefaultAsync(r => r.Id == 2);
            await oneRepo.FirstOrDefaultAsync(r => r.Id == 3);
            await oneRepo.FirstOrDefaultAsync(r => r.Id == 4);

            Assert.Pass();
        }
        void Samples()
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
    }
}