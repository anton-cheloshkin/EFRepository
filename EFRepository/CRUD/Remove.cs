namespace EFRepository.Internals
{
    public abstract partial class AbstractEFRepository<T>
    {
        public virtual async ValueTask Remove(T item)
        {
            if (!await Validate(item))
                return;

            return;

        }
        public virtual async ValueTask Remove(IList<T> items)
        {
            return;
        }
    }
}
