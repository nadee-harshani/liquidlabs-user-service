using LiquidLabs.UserService.Domain.Entities;
using LiquidLabs.UserService.Domain.Interfaces;
using LiquidLabs.UserService.Services.Services.Interfaces;

namespace LiquidLabs.UserService.Services.Services
{
    /// <summary>
    /// Service implementing user-related business logic.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private readonly IAddressRepository _address;
        private readonly IUnitOfWork _uow;

        public UserService(IUserRepository user,IAddressRepository address,IUnitOfWork uow)
        {
            _user    = user;
            _address = address;
            _uow     = uow;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            await _uow.BeginAsync();
            var users =  await _user.GetAll();
            await _uow.DisposeAsync();
            return users;
        }

        public async Task<User?> GetById(int id)
        {
            await _uow.BeginAsync();
            var user =  await _user.GetById(id);
            await _uow.DisposeAsync();
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            await _uow.BeginAsync();

            try
            {
                user = await _user.AddUser(user);

                user.Address.UserId = user.Id;
                await _address.AddAddress(user.Address);

                await _uow.CommitAsync();
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }

            return user;
        }
    }
}
