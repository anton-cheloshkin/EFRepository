using System.Linq.Expressions;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
        // one field primary key
        protected Expression<Func<TEntity, TEntity, bool>> LambdaEqualProp<TProp1, TProp2>(
            Expression<Func<TEntity, TProp1>> expression1,
            Expression<Func<TEntity, TProp2>> expression2
        )
        {
            var param = Parameter("p");
            var val = Parameter("v");

            var prop1 = Prop(expression1);
            var prop2 = Prop(expression2);

            var eq1 = Expression.Equal(Getter(param, prop1), Getter(val, prop1));
            var eq2 = Expression.Equal(Getter(param, prop2), Getter(val, prop2));

            var bin = Expression.And(eq1, eq2);

            return Expression.Lambda<Func<TEntity, TEntity, bool>>(bin, param, val);
        }
        protected Expression<Func<TEntity, TProp1, TProp2, bool>> LambdaEqualValue<TProp1, TProp2>(
            Expression<Func<TEntity, TProp1>> expression1,
            Expression<Func<TEntity, TProp2>> expression2
        )
        {
            var prop1 = Prop(expression1);
            var prop2 = Prop(expression2);

            var param = Parameter("p");
            var val1 = Expression.Variable(prop1.PropertyType, "v1");
            var val2 = Expression.Variable(prop2.PropertyType, "v2");

            var eq1 = Expression.Equal(Getter(param, prop1), val1);
            var eq2 = Expression.Equal(Getter(param, prop2), val2);

            var bin = Expression.And(eq1, eq2);

            return Expression.Lambda<Func<TEntity, TProp1, TProp2, bool>>(bin, param, val1, val2);
        }
    }
}
