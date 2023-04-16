namespace EFRepository
{
    public partial class EFDataProvider
    {
        public async ValueTask Save()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
