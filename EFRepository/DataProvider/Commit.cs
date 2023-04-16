namespace EFRepository
{
    public partial class EFDataProvider
    {
        protected virtual ValueTask BeforeCommit() => ValueTask.CompletedTask;
        protected virtual ValueTask AfterCommit() => ValueTask.CompletedTask;
        public async ValueTask Commit()
        {
            if (Transaction != null)
            {
                await BeforeCommit();
                await Transaction.CommitAsync();
                await AfterCommit();
            }
        }
    }
}
