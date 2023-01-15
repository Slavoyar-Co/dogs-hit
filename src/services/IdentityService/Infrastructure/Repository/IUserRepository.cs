using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositroy;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public Task<EAuthorizationStatus> ValidateUserCredentialsByLoginAsync(string login, string password);
        public Task<EAuthorizationStatus> ValidateUserCredentialsByEmailAsync(string email, string password);
        public Task<ERegistrationStatus> RegisterUserAsync(User user);
    }
}
