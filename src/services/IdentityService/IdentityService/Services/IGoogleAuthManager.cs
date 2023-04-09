using IdentityService.Services.Models;

namespace IdentityService.Services
{
    public interface IGoogleAuthManager
    {
        public Task<GoogleUserInfo> ValidateTokenAsync(string idToken);
    }
}
