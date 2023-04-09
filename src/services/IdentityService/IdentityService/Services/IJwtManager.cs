using Domain.Entities;

namespace IdentityService.Services
{
    public interface IJwtManager
    {
        public Task<string> GenerateAccessTokenAsync(User user);
    }
}
