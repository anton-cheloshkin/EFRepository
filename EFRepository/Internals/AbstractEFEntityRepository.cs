using Microsoft.EntityFrameworkCore;

namespace EFRepository.Internals
{
    public abstract partial class AbstractEFRepository<T> : IEFRepository<T>
        where T : class
    {
        protected static Type TSoftDeletable = typeof(ISoftDeletable);
        public abstract EntitySelectorModel<T> SelectorModel { get; init; }
#nullable disable
        protected readonly IEFDataProvider Provider;
        protected readonly DbSet<T> _table;
        //protected readonly Type EntityType;
        public AbstractEFRepository(EFDataProvider provider)
        {
            Provider = provider;
            _table = provider.Repo<T>();
        }
        public DbSet<T> Table => _table;
        public IQueryable<T> NoTrack => _table.AsNoTracking();
    }
}
