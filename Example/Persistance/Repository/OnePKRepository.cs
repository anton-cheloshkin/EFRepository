using EFRepository;
using Example.Persistance.Models;

namespace Example.Persistance.Repository
{
    public class OnePKRepository : EFRepository<OnePKEntity>
    {
        static EntitySelectorModel<OnePKEntity, int> _selector = new EntitySelectorModel<OnePKEntity, int>(r => r.Id);
        public override EntitySelectorModel<OnePKEntity> SelectorModel { get; init; }
        public OnePKRepository(IEFDataProvider provider) : base(provider) { SelectorModel = _selector; }

    }
}
