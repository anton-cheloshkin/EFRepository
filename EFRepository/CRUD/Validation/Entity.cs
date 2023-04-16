namespace EFRepository.Internals
{
    public abstract partial class AbstractEFRepository<T>
    {
        protected virtual ValueTask<bool> Validate(T item) => ValueTask.FromResult(true);

        protected virtual async ValueTask<bool> Validate(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (!await Validate(item)) return false;
            }
            return true;
        }
        protected virtual ValueTask AfterSave() => ValueTask.CompletedTask;
        protected virtual ValueTask AfterSave(T item) => ValueTask.CompletedTask;
        protected virtual ValueTask AfterSave(IEnumerable<T> items) => ValueTask.CompletedTask;
    }
}
