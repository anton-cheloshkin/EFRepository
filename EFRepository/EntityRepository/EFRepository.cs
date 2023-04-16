using EFRepository.Internals;

namespace EFRepository
{
    public abstract class EFRepository<TEntity> : AbstractEFRepository<TEntity>
        where TEntity : class
    {
        public EFRepository(IEFDataProvider provider) : base((EFDataProvider)provider)
        {
            if (TSoftDeletable.IsAssignableFrom(typeof(TEntity)))
            {
                throw new InvalidOperationException("ISoftDeletable: use ISoftDeletableEntityProvider");
            }
        }
    }
}
