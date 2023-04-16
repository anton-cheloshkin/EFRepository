using Microsoft.EntityFrameworkCore.Storage;

namespace EFRepository
{
    public interface IEFContext { }
    public interface IEFDataProvider : IDisposable
    {
        ValueTask<IDbContextTransaction?> BeginTransaction();
        ValueTask<IDbContextTransaction?> BeginTransaction(IDbContextTransaction tr);
        ValueTask Commit();
        ValueTask Rollback();
        ValueTask Save();
    }
}
