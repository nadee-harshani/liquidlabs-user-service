using Dapper;
using LiquidLabs.UserService.Domain.Entities;
using LiquidLabs.UserService.Domain.Interfaces;

namespace LiquidLabs.UserService.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing user data access operations, utilizing the Unit of Work pattern for transaction management.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _uow;

        public UserRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var sql = @"SELECT u.*, a.*
                        FROM Users u
                        INNER JOIN Addresses a ON u.Id = a.UserId";

            var user = await _uow.Connection.QueryAsync<User, Address,User>(
                sql,
                (u, a) =>
                {
                    u.Address = a;
                    return u;
                },
                transaction: _uow.Transaction,
                splitOn:"Id"
            );

            return user.ToList();
        }

        public async Task<User?> GetById(int id)
        {
            var sql        = @"SELECT u.*, a.*
                            FROM Users u
                            INNER JOIN Addresses a ON u.Id = a.UserId
                            WHERE u.Id = @Id";
            var user = await _uow.Connection.QueryAsync<User, Address, User>(
                        sql,
                        (u, a) => { u.Address = a; return u; },
                        new { Id = id },
                        transaction: _uow.Transaction,
                        splitOn: "Id"
                    );
            return user.FirstOrDefault();
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                var sql = @"INSERT INTO Users 
                    (Name, Username, Email, Phone, Website)
                    VALUES 
                    (@Name, @Username, @Email, @Phone, @Website);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                user.Id = await _uow.Connection.ExecuteScalarAsync<int>(sql, new
                {
                    user.Name,
                    user.Username,
                    user.Email,
                    user.Phone,
                    user.Website,
                }, _uow.Transaction);

                return user;
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw;
            }

        }
    }
}
