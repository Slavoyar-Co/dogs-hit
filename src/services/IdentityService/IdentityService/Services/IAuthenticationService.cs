
namespace IdentityService.Services
{
    public interface IAuthenticationService
    {
        public Task<string?> AuthentificateByCredentialsAsync(string username, string password); 
        public Task<string?> AuthenticateViaGoogleAsync(string token); 
    }
}
