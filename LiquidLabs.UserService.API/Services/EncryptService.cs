using Microsoft.AspNetCore.DataProtection;

namespace LiquidLabs.UserService.API.Services
{
    public class EncryptService
    {
        private readonly IDataProtector _protector;
        public EncryptService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("ApiKeyProtector");
        }

        public string Encrypt(string apiKey) => _protector.Protect(apiKey);

        public string Decrypt(string encryptKey) => _protector.Unprotect(encryptKey);


    }
}
