using System.Linq.Expressions;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
        protected Func<TEntity, TProp, bool> LambdaEqualValue<TProp>(Expression<Func<TEntity, TProp>> expression)
            => ExpressionEqualValue(expression).Compile();
    }
}
