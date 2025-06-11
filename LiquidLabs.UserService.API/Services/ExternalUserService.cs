using LiquidLabs.UserService.API.Dtos;
using System.Text.Json;

namespace LiquidLabs.UserService.API.Services
{
    /// <summary>
    /// Service for retrieving user information from an external API.
    /// Utilizes HttpClient to send HTTP requests and deserialize user data.
    /// </summary>
    public class ExternalUserService : IExternalUserService
    {
        private readonly HttpClient _httpClient;

        public ExternalUserService(HttpClient httpClient)
        {
            _httpClient    = httpClient;
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"users/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
