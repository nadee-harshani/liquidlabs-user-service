using Microsoft.Data.SqlClient;

namespace LiquidLabs.UserService.DataAccess.Persistence
{
    public interface IDatabaseConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
