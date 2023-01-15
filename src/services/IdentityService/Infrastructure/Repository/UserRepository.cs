using Domain.Entities;
using Domain.Enums;
using Infrastructure.PasswordCrypters;
using Infrastructure.Repository;
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

        private async Task<EAuthorizationStatus> Validate(Expression<Func<User, bool>> predicate, string password)
        {
            var user = await _identityDbContext.Users.Where(predicate).FirstOrDefaultAsync();
            if (user is null)
            {
                //TODO check predicate assign variable
                return EAuthorizationStatus.LoginError; 
            }

            var isVerified = CryptHelper.VerifyPassword(password, user.Password);
            if (!isVerified)
            {
                return EAuthorizationStatus.PasswordError;
            }
            return EAuthorizationStatus.Success;
        }

        public async Task<EAuthorizationStatus> ValidateUserCredentialsByEmailAsync(string email, string password)
        {
            return await Validate(x => x.Email.Equals(email), password);
        }

        public async Task<EAuthorizationStatus> ValidateUserCredentialsByLoginAsync(string login, string password)
        {
            return await Validate(x => x.Login.Equals(login), password);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _identityDbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<ERegistrationStatus> RegisterUserAsync(User user)
        {
            if (await CheckIfExits(user))
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

        private async Task<bool> CheckIfExits(User user)
        {
            return await GetByIdAsync(user.Id) != null;
        }
    }
}