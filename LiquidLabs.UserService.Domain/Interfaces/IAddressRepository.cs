using LiquidLabs.UserService.Domain.Entities;

namespace LiquidLabs.UserService.Domain.Interfaces
{
    /// <summary>
    /// Interface defining data access operations for Address entities
    /// </summary>
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAll();

        Task<Address?> GetById(int id);

        Task<Address> AddAddress(Address address);
    }
}
