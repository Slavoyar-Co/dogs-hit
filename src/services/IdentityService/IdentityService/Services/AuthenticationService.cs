using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repository;

namespace IdentityService.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtManager _jwtManager;
        private readonly IGoogleAuthManager _googleAuthManager;

        public AuthenticationService(IUserRepository userRepository, IJwtManager jwtManager, IGoogleAuthManager googleAuthManager)
        {
            _userRepository = userRepository;
            _jwtManager = jwtManager;
            _googleAuthManager = googleAuthManager;
        }

        public async Task<string?> AuthentificateByCredentialsAsync(string username, string password)
        {
            var response = await _userRepository.GetByUserNameAndPasswordAsync(username, password);

            if (response.AuthorizationStatus == EAuthorizationStatus.Success)
            {
                return await _jwtManager.GenerateAccessTokenAsync(response.User!);
            }

            return null;
        }

        public async Task<string?> AuthenticateViaGoogleAsync(string token)
        {
            var userInfo = await _googleAuthManager.ValidateTokenAsync(token);

            if(userInfo is null)
            {
                return null;//TODO refactor
            }

            var user = await _userRepository.GetByUserNameAsync(userInfo.Name);
            if (user is null) 
            {
                user = new User 
                {
                    Id = Guid.NewGuid(),
                    Name = userInfo.Name,
                    Login = userInfo.Name,
                    Email = userInfo.Email 
                };
                await _userRepository.CreateUserAsync(user);
            }


            return await _jwtManager.GenerateAccessTokenAsync(user); 
        }
    }
}
