using Microsoft.EntityFrameworkCore;

namespace EFRepository
{
    public static partial class IQueryableExtension
    {
#nullable disable
        public static IQueryable<T> WithPK<T, TKey>(this IEFRepository<T> repo, TKey keyValue, bool track = false) where T : class
            => repo.Where((repo.SelectorModel as EntitySelectorModel<T, TKey>).WithPrimaryKey(keyValue), track);
        public static IQueryable<T> WithPK<T, TKey1, TKey2>(this IEFRepository<T> repo, TKey1 keyValue1, TKey2 keyValue2, bool track = false) where T : class
            => repo.Where((repo.SelectorModel as EntitySelectorModel<T, TKey1, TKey2>).WithPrimaryKey(keyValue1, keyValue2), track);
        public static async ValueTask<T> Get<T, TKey>(this IEFRepository<T> repo, TKey key) where T : class
            => await repo.WithPK(key).FirstOrDefaultAsync();
        public static async ValueTask<T> Get<T, TKey1, TKey2>(this IEFRepository<T> repo, TKey1 keyValue1, TKey2 keyValue2) where T : class
            => await repo.WithPK(keyValue1, keyValue2).FirstOrDefaultAsync();
    }
}
