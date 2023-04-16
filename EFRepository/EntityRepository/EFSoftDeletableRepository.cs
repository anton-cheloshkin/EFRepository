using EFRepository.Internals;

namespace EFRepository
{
    interface IEFSoftDeletableRepository { }
    public abstract class EFSoftDeletableRepository<TEntity> : AbstractEFRepository<TEntity>, IEFSoftDeletableRepository
        where TEntity : class, ISoftDeletable
    {
        public EFSoftDeletableRepository(IEFDataProvider provider) : base((EFDataProvider)provider)
        {
        }
        public override async ValueTask Remove(TEntity item)
        {
            item.Deleted = true;
            await Update(item);
        }
        public override async ValueTask Remove(IList<TEntity> items)
        {
            foreach (var item in items)
                item.Deleted = true;

            await Update(items);
        }
    }
}
