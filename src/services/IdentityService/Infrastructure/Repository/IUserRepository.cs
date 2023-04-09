using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositroy;
using Infrastructure.ResponseEntities;

namespace Infrastructure.Repository
{
    public interface IUserRepository : IAsyncRepositoryBase<User>
    {
        public Task<UserRepositoryResponse> GetByUserNameAndPasswordAsync(string useName, string password);
        public Task<UserRepositoryResponse> GetByEmailAndPasswordAsync(string email, string password);
        public Task<User> GetByUserNameAsync(string userName);
        public Task<User> GetByEmailAsync(string email);
        public Task<ERegistrationStatus> CreateUserAsync(User user);
    }
}
