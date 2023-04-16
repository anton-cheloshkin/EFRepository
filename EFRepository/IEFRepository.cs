using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFRepository
{
    public interface IEFRepository<TEntity> where TEntity : class
    {
#nullable disable
        EntitySelectorModel<TEntity> SelectorModel { get; init; }
        DbSet<TEntity> Table { get; }
        IQueryable<TEntity> NoTrack { get; }
        ValueTask<EntityEntry<TEntity>> Insert(TEntity item);
        ValueTask<List<TEntity>> Insert(IList<TEntity> item);
        ValueTask<EntityEntry<TEntity>> Update(TEntity item);
        ValueTask<List<TEntity>> Update(IList<TEntity> item);
        ValueTask Remove(TEntity item);
        ValueTask Remove(IList<TEntity> items);
        ValueTask<EntityEntry<TEntity>> UpSert(TEntity item);
        ValueTask<List<TEntity>> UpSert(IList<TEntity> item);
    }
}
