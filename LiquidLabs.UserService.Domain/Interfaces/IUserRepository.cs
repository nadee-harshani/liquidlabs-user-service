using LiquidLabs.UserService.Domain.Entities;

namespace LiquidLabs.UserService.Domain.Interfaces
{
    /// <summary>
    /// Interface defining data access operations for User entities.
    /// </summary>
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User?> GetById(int id);

        Task<User> AddUser(User user);
    }
}
