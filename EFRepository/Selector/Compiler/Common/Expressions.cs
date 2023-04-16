using System.Linq.Expressions;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
        protected Expression<Func<TEntity, TEntity, bool>> ExpressionEqualProp<TProp>(Expression<Func<TEntity, TProp>> expression)
        {
            var prop = Prop(expression);
            var bin = Expression.Equal(Getter("entry", prop), Getter("search", prop));
            return Expression.Lambda<Func<TEntity, TEntity, bool>>(bin);
        }
        protected Expression<Func<TEntity, TProp, bool>> ExpressionEqualValue<TProp>(Expression<Func<TEntity, TProp>> expression)
        {
            var prop = Prop(expression);
            var bin = Expression.Equal(
                Getter("entry", prop),
                Expression.Variable(prop.PropertyType)
            );
            return Expression.Lambda<Func<TEntity, TProp, bool>>(bin);
        }
    }
}
