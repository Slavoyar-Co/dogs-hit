using Google.Apis.Auth.OAuth2.Responses;
using IdentityService.Services.Models;

namespace IdentityService.Services.Contracts
{
    public interface IGoogleAuthManager
    {
        public Task<GoogleUserInfo> ValidateTokenAsync(string idToken);

        public Task<TokenResponse> GetFlowToken(GoogleSignInModel googleSignInModel);
    }
}
