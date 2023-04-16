using System.Linq.Expressions;
using System.Reflection;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity>
    {
#nullable disable
        MemberExpression Getter(string name, PropertyInfo prop)
            => Expression.Property(Expression.Parameter(typeof(TEntity), name), prop.Name);
        PropertyInfo Prop<TProp>(Expression<Func<TEntity, TProp>> expression) => ((MemberExpression)expression.Body).Member as PropertyInfo;
    }
}
