using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFRepository
{
    public static partial class IQueryableExtension
    {
#nullable disable
        public static IQueryable<T> Query<T>(this IEFRepository<T> repo, bool track = false) where T : class
            => track ? repo.Table : repo.NoTrack;
        public static IQueryable<T> Where<T>(this IEFRepository<T> repo, Func<IQueryable<T>, IQueryable<T>> predicate, bool track = false) where T : class
            => predicate(repo.Query(track));
        public static IQueryable<T> Where<T>(this IEFRepository<T> repo, Expression<Func<T, bool>> expression, bool track = false) where T : class
            => repo.Query(track).Where(expression);
        public static async ValueTask<T?> FirstOrDefaultAsync<T>(this IEFRepository<T> repo, Expression<Func<T, bool>> predicate = default, bool track = false) where T : class
            => await repo.Query(track).FirstOrDefaultAsync(predicate);
    }
}
