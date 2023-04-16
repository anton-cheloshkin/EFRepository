namespace EFRepository.Internals
{
    public abstract partial class AbstractEFEntityProvider<T>
    {
        protected T Un(T entity)
        {
            var typeEntry = entity.GetType();
            if (typeEntry.BaseType == EntityType)
            {
                return (T)entity;
            }
            return entity;
        }
        protected IList<T> Un(IEnumerable<T> list)
        {
            var first = list.FirstOrDefault();
            var typeEntry = first.GetType();
            if (typeEntry.BaseType == EntityType)
            {
                return list.Select(r => (T)r).ToList();
            }
            return list.ToList();
        }
        bool IsProxy(T item)
        {
            var tItem = item.GetType();
            return tItem.IsSubclassOf(EntityType) && tItem.GetProperty("LazyLoader") != null;
        }
        protected async ValueTask<T> GetProxied(T item)
        {
            if (IsProxy(item)) return item;


            var proxy = await this.FirstOrDefaultAsync(SelectorModel.Invoke(item));
            if (proxy == null)
                throw new InvalidDataException();

            return proxy;
        }
        protected async ValueTask<IEnumerable<T>> GetProxied(IEnumerable<T> items)
        {
            var unproxied = items.Where(r => !IsProxy(r));

            if (unproxied.Count() == 0)
                return items;

            var unproxiedIds = unproxied.Select(r => r.Id);
            var proxies = await Get(unproxiedIds);
            return items
                .Select(r => proxies.FirstOrDefault(p => p.Id == r.Id) ?? r);
        }
    }
}
