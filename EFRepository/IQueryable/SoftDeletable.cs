namespace EFRepository
{
    public static partial class IQueryableExtension
    {
        public static IQueryable<T> Deleted<T>(this IQueryable<T> query, bool? del) where T : ISoftDeletable
        {
            if (del == null) return query;
            return query.Where(r => r.Deleted == del);
        }
        public static IQueryable<T> Deleted<T>(this IQueryable<T> query) where T : ISoftDeletable
            => query.Where(r => r.Deleted);
        public static IQueryable<T> Deleted<T>(this IQueryable<T> query, bool del) where T : ISoftDeletable
            => query.Where(r => r.Deleted == del);

        public static IQueryable<T> NoDel<T>(this IQueryable<T> query) where T : ISoftDeletable
            => query.Where(r => !r.Deleted);
        public static IEnumerable<T> NoDel<T>(this IEnumerable<T> query) where T : ISoftDeletable
            => query.AsQueryable().NoDel();

    }
}
