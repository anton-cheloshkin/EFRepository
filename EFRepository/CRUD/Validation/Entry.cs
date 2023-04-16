using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFRepository.Internals
{
    public abstract partial class AbstractEFRepository<T>
    {
        protected virtual bool Validate(EntityEntry<T> entry) => entry != null;

        protected virtual bool Validate(IEnumerable<EntityEntry<T>> entries)
        {
            foreach (var entry in entries)
            {
                if (!Validate(entry)) return false;
            }
            return true;
        }
        protected virtual void BeforeSave(EntityEntry<T> entity)
        {
        }
    }
}
