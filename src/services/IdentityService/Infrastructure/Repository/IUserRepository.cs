using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositroy;
using Infrastructure.ResponseEntities;

namespace Infrastructure.Repository
{
    public interface IUserRepository : IAsyncRepositoryBase<User>
    {
        public Task<UserRepositoryResponse> GetByUserNameAsync(string username, string password);
        public Task<UserRepositoryResponse> GetByEmailAsync(string email, string password);
        public Task<ERegistrationStatus> RegisterUserAsync(User user);
    }
}
