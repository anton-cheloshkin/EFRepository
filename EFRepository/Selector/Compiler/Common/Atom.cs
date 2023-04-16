using System.Linq.Expressions;
using System.Reflection;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
#nullable disable
        protected ParameterExpression Parameter(string name) => Expression.Parameter(typeof(TEntity), name);
        protected MemberExpression Getter(ParameterExpression parameter, PropertyInfo prop)
            => Expression.Property(parameter, prop.Name);
        protected MemberExpression Getter(string name, PropertyInfo prop)
            => Expression.Property(Expression.Parameter(typeof(TEntity), name), prop.Name);
        protected PropertyInfo Prop<TProp>(Expression<Func<TEntity, TProp>> expression) => ((MemberExpression)expression.Body).Member as PropertyInfo;
    }
}
