using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using IdentityService.Services.Models;

namespace IdentityService.Services
{
    public class GoogleAuthManager : IGoogleAuthManager
    {
        private IConfiguration _configuration;

        public GoogleAuthManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleUserInfo> GetFlowToken(GoogleSignInModel googleSignIn)
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _configuration["Authentication:Google:ClientId"]!,
                     ClientSecret = _configuration["Authentication:Google:ClientSecret"]!
             },
                Scopes = new[] { Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile },
            });

            // exchange the authorization code for tokens
            var tokenResponse = await flow.ExchangeCodeForTokenAsync(
                "",
                googleSignIn.Code,
                "https://localhost:7299/api/authorization/google",
                CancellationToken.None
                );
        }

        public async Task<GoogleUserInfo> ValidateTokenAsync(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

                return new GoogleUserInfo
                {
                    Id = payload.Subject,
                    Email = payload.Email,
                    Name = payload.GivenName,
                    Locale = payload.Locale,
                    PictureUrl = payload.Picture
                };
            }
            catch (InvalidJwtException e)
            {
                throw new Exception("Invalid token", e);
            }
        }
    }
}
