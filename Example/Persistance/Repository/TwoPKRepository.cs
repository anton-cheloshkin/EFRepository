using EFRepository;
using Example.Persistance.Models;

namespace Example.Persistance.Repository
{
    public class TwoPKRepository : EFRepository<TwoPKEntity>
    {
        static EntitySelectorModel<TwoPKEntity, int, TestEnum> _selector = new EntitySelectorModel<TwoPKEntity, int, TestEnum>(r => r.Id, r => r.Second);
        public override EntitySelectorModel<TwoPKEntity> SelectorModel { get; init; }
        public TwoPKRepository(IEFDataProvider provider) : base(provider) { SelectorModel = _selector; }

    }
}
