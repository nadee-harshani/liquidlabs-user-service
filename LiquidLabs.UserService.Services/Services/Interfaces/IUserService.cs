using LiquidLabs.UserService.Domain.Entities;

namespace LiquidLabs.UserService.Services.Services.Interfaces
{
    /// <summary>
    /// Interface defining business logic operations for User entities.
    /// </summary>
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();

        Task<User?> GetById(int id);

        Task<User> AddUser(User user);
    }
}
