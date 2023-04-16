using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFRepository
{
    public abstract partial class EFDataProvider : IEFDataProvider
    {
#nullable disable
        protected virtual DbContext _ctx { get; }
        public IDbContextTransaction? Transaction
            => _ctx.Database.CurrentTransaction;
        public EFDataProvider(DbContext ctx)
        {
            _ctx = ctx;
        }
        public DbSet<T> Repo<T>() where T : class => _ctx.Set<T>();
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
