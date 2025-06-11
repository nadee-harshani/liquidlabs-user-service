using System.Data;

namespace LiquidLabs.UserService.Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
