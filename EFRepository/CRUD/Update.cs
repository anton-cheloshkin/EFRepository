using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFRepository.Internals
{
    public abstract partial class AbstractEFRepository<T>
    {
        public virtual async ValueTask<EntityEntry<T>> Update(T item)
        {
            if (!await Validate(item))
                return default;

            return default;

        }
        public virtual async ValueTask<List<T>> Update(IList<T> items)
        {
            return default;
        }
    }
}
