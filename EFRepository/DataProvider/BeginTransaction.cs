using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFRepository
{
    public partial class EFDataProvider
    {
        public async ValueTask<IDbContextTransaction?> BeginTransaction()
        {
            if (Transaction == null)
                await _ctx.Database.BeginTransactionAsync();

            return Transaction;
        }
        public async ValueTask<IDbContextTransaction?> BeginTransaction(IDbContextTransaction tr)
        {
            if (tr == null)
            {
                await BeginTransaction();
            }
            else
            {
                await _ctx.Database.UseTransactionAsync(tr.GetDbTransaction());
            }
            return Transaction;
        }
    }
}
