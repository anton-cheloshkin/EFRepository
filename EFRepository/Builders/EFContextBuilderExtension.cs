using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFRepository.Builders
{
    public static class EFContextBuilderExtension
    {
        public static EFContextBuilder<TContext, TContextImpl, TProvider, TProvoderImpl>
            AddEFContext<TContext, TContextImpl, TProvider, TProvoderImpl>(
                this IServiceCollection services,
                Action<DbContextOptionsBuilder>? contextBuider = null,
                ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
                ServiceLifetime optionsLifetime = ServiceLifetime.Scoped
            )
            where TContext : IEFContext
            where TContextImpl : DbContext, TContext
            where TProvider : IEFDataProvider
            where TProvoderImpl : class, IEFDataProvider
        {
            return new(services, contextBuider, contextLifetime, optionsLifetime);
        }
        public static EFContextBuilder<TContext, TContextImpl, TProvider, TProvoderImpl>
            AddEFContext<TContext, TContextImpl, TProvider, TProvoderImpl>(
                this IServiceCollection services,
                Action<IServiceProvider, DbContextOptionsBuilder> contextBuider,
                ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
                ServiceLifetime optionsLifetime = ServiceLifetime.Scoped
            )
            where TContext : IEFContext
            where TContextImpl : DbContext, TContext
            where TProvider : IEFDataProvider
            where TProvoderImpl : class, IEFDataProvider
        {
            return new(services, contextBuider, contextLifetime, optionsLifetime);
        }
    }
}
