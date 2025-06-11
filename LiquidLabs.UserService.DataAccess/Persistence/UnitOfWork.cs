using LiquidLabs.UserService.Domain.Interfaces;
using System.Data;
using System.Data.Common;

namespace LiquidLabs.UserService.DataAccess.Persistence
{
    /// <summary>
    /// Implements the Unit of Work pattern to manage database transactions and coordinate repository operations.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        public UnitOfWork(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public IDbConnection Connection => _connection ?? throw new InvalidOperationException("User UnitOfWork has not been started.");
        public IDbTransaction Transaction => _transaction ?? throw new InvalidOperationException("No active transaction.");


        public async Task BeginAsync()
        {
            if (_connection != null)
                throw new InvalidOperationException("UnitOfWork has already been started.");

            _connection = _connectionFactory.CreateConnection();
            if (_connection.State != ConnectionState.Open)
                await (_connection as DbConnection)!.OpenAsync();

            _transaction = _connection.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction to commit.");

            try
            {
                _transaction.Commit();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                await DisposeAsync();
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                await DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                if (_connection is DbConnection dbConn)
                    await dbConn.DisposeAsync();
                else
                    _connection.Dispose();

                _connection = null;
            }
        }
    }
}
