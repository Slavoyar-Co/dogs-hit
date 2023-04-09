using Domain.Entities;

namespace IdentityService.Services.Contracts
{
    /// <summary>
    /// Used for bearer token interaction
    /// </summary>
    public interface IJwtManager
    {
        /// <summary>
        /// Creates claims with bearer token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GenerateAccessTokenAsync(User user);
    }
}
