using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositroy;

namespace Infrastructure.Repository
{
    public interface IUserRepository : IAsyncRepositoryBase<User>
    {
        public Task<EAuthorizationStatus> ValidateUserCredentialsByLoginAsync(string login, string password);
        public Task<EAuthorizationStatus> ValidateUserCredentialsByEmailAsync(string email, string password);
        public Task<ERegistrationStatus> RegisterUserAsync(User user);
    }
}
