using Domain.Entities;
using Domain.Enums;
using Infrastructure.Crypt;
using Infrastructure.Repository;
using Infrastructure.ResponseEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositroy
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public UserRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<UserRepositoryResponse> GetByEmailAsync(string email, string password)
        {
            return await ValidateCredentials(x => x.Email!.Equals(email), password);
        }


        public async Task<UserRepositoryResponse> GetByUserNameAsync(string username, string password)
        {
            return await ValidateCredentials(x => x.Login.Equals(username), password);
        }


        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _identityDbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ERegistrationStatus> RegisterUserAsync(User user)
        {
            if (await CheckIfUserExits(user))
            {
                return ERegistrationStatus.AlreadyExists;
            }
            user.Password = CryptHelper.HashPassword(user.Password);
            await _identityDbContext.Users.AddAsync(user);
            await _identityDbContext.SaveChangesAsync();
            return ERegistrationStatus.Success;
        }


        public async Task<User> UpdateAsync(User user)
        {
            _identityDbContext.Update(user);
            await _identityDbContext.SaveChangesAsync();
            return user;
        }

        private async Task<UserRepositoryResponse> ValidateCredentials(Expression<Func<User, bool>> predicate, string password)
        {
            var user = await _identityDbContext.Users.Where(predicate).FirstOrDefaultAsync();

            if (user is null)
            {
                //TODO check predicate assign variable
                return new UserRepositoryResponse(user, EAuthorizationStatus.LoginError);
            }

            var isVerified = CryptHelper.VerifyPassword(password, user.Password);

            if (!isVerified)
            {
                return new UserRepositoryResponse(user, EAuthorizationStatus.WrongPassword);
            }

            return new UserRepositoryResponse(user, EAuthorizationStatus.Success);
        }

        private async Task<bool> CheckIfUserExits(User user)
        {
            return await _identityDbContext.Users.Where(x => x.Login == user.Login || x.Email == user.Email).AnyAsync();
        }
    }
}