namespace EFRepository
{
    public partial class EFDataProvider
    {
        protected virtual ValueTask BeforeRollback() => ValueTask.CompletedTask;
        protected virtual ValueTask AfterRollback() => ValueTask.CompletedTask;
        public async ValueTask Rollback()
        {
            if (Transaction != null)
            {
                await BeforeRollback();
                await Transaction.RollbackAsync();
                await AfterRollback();
            }
        }
    }
}
