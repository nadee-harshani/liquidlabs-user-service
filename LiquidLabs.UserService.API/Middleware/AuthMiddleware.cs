using LiquidLabs.UserService.API.Services;

namespace LiquidLabs.UserService.API.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EncryptService _encryptService;
        private const string APIKEY_HEADER = "X-Api-Key";
        private readonly string _encryptedKey;

        public AuthMiddleware(RequestDelegate next,EncryptService encryptService,IConfiguration congiguration)
        {
            _next = next;
            _encryptService = encryptService;
            _encryptedKey = congiguration["APIKEY"]??"";
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(!httpContext.Request.Headers.TryGetValue(APIKEY_HEADER,out var extractedApiKey))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync("API Key is not provided");
                return;
            }
            var decryptKey = _encryptService.Decrypt(_encryptedKey);

            if (!decryptKey.Equals(extractedApiKey))
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await httpContext.Response.WriteAsync("Unauthorized client.");
                return;
            }

            await _next(httpContext);
        }
    }
}
