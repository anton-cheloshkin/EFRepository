using System.Linq.Expressions;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
        // one field primary key
        protected Func<TEntity, TEntity, bool> CompilePrimaryKeyEntity<TProp>(Expression<Func<TEntity, TProp>> expression)
            => ExpressionEqualProp(expression).Compile();
        protected Func<TEntity, TProp, bool> CompilePrimaryKeyValue<TProp>(Expression<Func<TEntity, TProp>> expression)
            => ExpressionEqualValue(expression).Compile();
        // two fields primary key
        protected Func<TEntity, TEntity, bool> CompilePrimaryKeyEntity<TProp1, TProp2>(
            Expression<Func<TEntity, TProp1>> expression1,
            Expression<Func<TEntity, TProp2>> expression2
        )
            => Expression.Lambda<Func<TEntity, TEntity, bool>>(
                Expression.And(ExpressionEqualProp(expression1), ExpressionEqualProp(expression2))
            ).Compile();
        protected Func<TEntity, TProp1, TProp2, bool> CompilePrimaryKeyValue<TProp1, TProp2>(
            Expression<Func<TEntity, TProp1>> expression1,
            Expression<Func<TEntity, TProp2>> expression2
        )
            => Expression.Lambda<Func<TEntity, TProp1, TProp2, bool>>(
                Expression.And(ExpressionEqualValue(expression1), ExpressionEqualValue(expression2))
            ).Compile();
    }
}
