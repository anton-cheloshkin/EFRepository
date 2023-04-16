using System.Linq.Expressions;

namespace EFRepository
{
    public abstract partial class EntitySelectorModel<TEntity> where TEntity : class
    {
#nullable disable
        protected Func<TEntity, TEntity, bool> EntitySelector { get; init; }
        public virtual Expression<Func<TEntity, bool>> WithPrimaryKey(TEntity search)
        {
            return entry => EntitySelector(entry, search);
        }
    }
}
