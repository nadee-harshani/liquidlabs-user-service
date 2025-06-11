using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LiquidLabs.UserService.DataAccess.Persistence
{
    /// <summary>
    /// Factory responsible for creating SQL database connections using the configured connection string from the application settings.
    /// </summary>
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Database connection string is not configured.");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
