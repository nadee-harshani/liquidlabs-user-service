using Dapper;
using LiquidLabs.UserService.Domain.Entities;
using LiquidLabs.UserService.Domain.Interfaces;

namespace LiquidLabs.UserService.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing address data access operations,utilizing the Unit of Work pattern for transaction management.
    /// </summary>
    public class AddressRepository : IAddressRepository
    {
        private readonly IUnitOfWork _uow;

        public AddressRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            var sql = @"SELECT * FROM Addresses";

            var addresses = await _uow.Connection.QueryAsync<Address>(sql);
            return addresses.ToList();
        }

        public async Task<Address?> GetById(int id)
        {
            var sql = "SELECT * FROM Addresses WHERE Id = @Id";
            var address = await _uow.Connection.QueryFirstOrDefaultAsync<Address>(sql, new { Id = id });
            return address;
        }

        public async Task<Address> AddAddress(Address address)
        {
            var sql = @"INSERT INTO Addresses (UserId, Street, Suite, City, Zipcode)
                VALUES (@UserId, @Street, @Suite, @City, @Zipcode);
                SELECT CAST(SCOPE_IDENTITY() as int);"
            ;

            address.Id = await _uow.Connection.ExecuteScalarAsync<int>(sql, new
            {
                address.UserId,
                address.Street,
                address.Suite,
                address.City,
                address.Zipcode
            }, _uow.Transaction);

            return address;
        }
    }
}
