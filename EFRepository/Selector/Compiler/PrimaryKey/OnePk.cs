using System.Linq.Expressions;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
        protected Expression<Func<TEntity, TEntity, bool>> LambdaEqualProp<TProp>(Expression<Func<TEntity, TProp>> expression)
        {
            var param = Parameter("p");
            var val = Parameter("v");
            var prop = Prop(expression);
            var bin = Expression.Equal(Getter(param, prop), Getter(val, prop));
            return Expression.Lambda<Func<TEntity, TEntity, bool>>(bin, param, val);
        }
        protected Expression<Func<TEntity, TProp, bool>> LambdaEqualValue<TProp>(Expression<Func<TEntity, TProp>> expression)
        {
            var prop = Prop(expression);
            var param = Parameter("p");
            var val = Expression.Variable(prop.PropertyType, "v");

            var bin = Expression.Equal(Getter(param, prop), val);
            return Expression.Lambda<Func<TEntity, TProp, bool>>(bin, param, val);
        }
    }
}
