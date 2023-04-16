using System.Linq.Expressions;

namespace EFRepository
{
    public partial class EntitySelectorModel<TEntity, TProp> : EntitySelectorModel<TEntity> where TEntity : class
    {
        protected Func<TEntity, TProp, bool> ValueSelector { get; init; }
        public EntitySelectorModel(Expression<Func<TEntity, TProp>> expression)
        {
            EntitySelector = CompilePrimaryKeyEntity(expression);
            ValueSelector = CompilePrimaryKeyValue(expression);

        }
        public virtual Expression<Func<TEntity, bool>> WithPrimaryKey(TProp value)
        {
            return entry => ValueSelector(entry, value);
        }
    }
    public partial class EntitySelectorModel<TEntity, TProp1, TProp2> : EntitySelectorModel<TEntity> where TEntity : class
    {
        protected Func<TEntity, TProp1, TProp2, bool> ValueSelector { get; init; }
        public EntitySelectorModel(Expression<Func<TEntity, TProp1>> expression1, Expression<Func<TEntity, TProp2>> expression2)
        {
            EntitySelector = CompilePrimaryKeyEntity(expression1, expression2);
            ValueSelector = CompilePrimaryKeyValue(expression1, expression2);

        }
        public virtual Expression<Func<TEntity, bool>> WithPrimaryKey(TProp1 value1, TProp2 value2)
        {
            return entry => ValueSelector(entry, value1, value2);
        }
    }
}
