namespace IdentityService.Services
{
    public interface IJwtAuthentificationManager
    {
        public Task<string> AuthentificateAsync(string username, string password);
    }
}
