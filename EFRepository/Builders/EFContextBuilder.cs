using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFRepository.Builders
{
    public class EFContextBuilder<TContext, TContextImpl, TProvider, TProvoderImpl>
        where TContext : IEFContext
        where TContextImpl : DbContext, TContext
        where TProvider : IEFDataProvider
        where TProvoderImpl : class, IEFDataProvider
    {
        readonly IServiceCollection Services;
        public EFContextBuilder(
            IServiceCollection services,
            Action<DbContextOptionsBuilder>? optionsAction = null,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped
        )
        {
            Services = services;
            Services.AddDbContext<TContext, TContextImpl>(optionsAction, contextLifetime, optionsLifetime);
            Init();
        }
        public EFContextBuilder(
            IServiceCollection services, Action<IServiceProvider,
            DbContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped
        )
        {
            Services = services;
            Services.AddDbContext<TContext, TContextImpl>(optionsAction, contextLifetime, optionsLifetime);
            Init();
        }
        void Init()
        {
            Services.AddScoped<IEFDataProvider, TProvoderImpl>();
        }
        public EFContextBuilder<TContext, TContextImpl, TProvider, TProvoderImpl> AddRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : class, IEFRepository<TEntity>
        {
            Services.AddScoped<IEFRepository<TEntity>, TRepository>();
            return this;
        }
    }
}
