using System.Linq.Expressions;

namespace EFRepository
{
    public partial class EntitySelectorModel<TEntity, TProp> : EntitySelectorModel<TEntity> where TEntity : class
    {
        ParameterExpression Param;
        MemberExpression EntityProp;
        public EntitySelectorModel(Expression<Func<TEntity, TProp>> expression)
        {
            EntitySelectorCompiled = CompilePrimaryKeyEntity(expression);

            var prop = Prop(expression);
            Param = Parameter("entity");
            EntityProp = Getter(Param, prop);
        }
        public virtual Expression<Func<TEntity, bool>> WithPrimaryKey(TProp value)
        {
            var constant = Expression.Constant(value);
            var body = Expression.Equal(EntityProp, constant);
            return Expression.Lambda<Func<TEntity, bool>>(body, Param);
        }
    }
    public partial class EntitySelectorModel<TEntity, TProp1, TProp2> : EntitySelectorModel<TEntity> where TEntity : class
    {
        protected Func<TEntity, TProp1, TProp2, bool> ValueSelectorCompiled { get; init; }
        public EntitySelectorModel(Expression<Func<TEntity, TProp1>> expression1, Expression<Func<TEntity, TProp2>> expression2)
        {
            EntitySelectorCompiled = CompilePrimaryKeyEntity(expression1, expression2);
            ValueSelectorCompiled = CompilePrimaryKeyValue(expression1, expression2);
        }
        public virtual Expression<Func<TEntity, bool>> WithPrimaryKey(TProp1 value1, TProp2 value2)
        {
            return entry => ValueSelectorCompiled(entry, value1, value2);
        }
    }
}
