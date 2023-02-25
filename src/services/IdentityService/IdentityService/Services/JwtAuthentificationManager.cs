using Domain.Entities;
using IdentityService.Services;
using Infrastructure.Repository;
using Infrastructure.Repositroy;
using Infrastructure.ResponseEntities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Implementations
{
    public class JwtAuthentificationManager : IJwtAuthentificationManager
    {
        private readonly IUserRepository _userRepository;
        private readonly string _key;

        public JwtAuthentificationManager(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _key = configuration.GetSection("Keys:JwtKey").Value!;
        }

        public async Task<string> AuthentificateAsync(string username, string password)
        {
            var response = await _userRepository.GetByUserNameAsync(username, password);
            user.

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.),
                    new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(tokenKey),
                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }
    }
}