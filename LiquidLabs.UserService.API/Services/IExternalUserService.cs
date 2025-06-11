using LiquidLabs.UserService.API.Dtos;

namespace LiquidLabs.UserService.API.Services
{
    public interface IExternalUserService
    {
        Task<UserDto?> GetUserById(int id);
    }
}
