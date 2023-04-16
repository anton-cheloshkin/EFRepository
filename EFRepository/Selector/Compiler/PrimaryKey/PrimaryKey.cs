using System.Linq.Expressions;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
        // one field primary key
        protected Func<TEntity, TEntity, bool> CompilePrimaryKeyEntity<TProp>(Expression<Func<TEntity, TProp>> expression)
            => LambdaEqualProp(expression).Compile();
        protected Func<TEntity, TProp, bool> CompilePrimaryKeyValue<TProp>(Expression<Func<TEntity, TProp>> expression)
            => LambdaEqualValue(expression).Compile();
        // two fields primary key
        protected Func<TEntity, TEntity, bool> CompilePrimaryKeyEntity<TProp1, TProp2>(
            Expression<Func<TEntity, TProp1>> expression1,
            Expression<Func<TEntity, TProp2>> expression2
        )
            => LambdaEqualProp(expression1, expression2).Compile();
        protected Func<TEntity, TProp1, TProp2, bool> CompilePrimaryKeyValue<TProp1, TProp2>(
            Expression<Func<TEntity, TProp1>> expression1,
            Expression<Func<TEntity, TProp2>> expression2
        )
            => LambdaEqualValue(expression1, expression2).Compile();
    }
}
