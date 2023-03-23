using Domain.Entities;
using Domain.Enums;
using IdentityService.Controllers.Dtos;
using IdentityService.Services;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtAuthentificationManager _jwtAuthentificationManager;

        private readonly IUserRepository _userRepository;
        public AuthorizationController(IUserRepository userRepository,
            IJwtAuthentificationManager jwtAuthentificationManager)
        {
            _userRepository = userRepository;
            _jwtAuthentificationManager = jwtAuthentificationManager;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("user")]
        public ActionResult<UserDto> GetUser()
        {



            var user = new UserDto
            {
                Name = User?.Identity?.Name,
                Id = Guid.Parse(User!.Claims.First(x => x.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value!),

            };

            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<RegisterUserDto>> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = userDto.Login,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };

            var status = await _userRepository.RegisterUserAsync(user);

            if (status == ERegistrationStatus.Success)
            {
                userDto.Id = user.Id;
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("bearer-token")]
        public async Task<ActionResult<string>> Authentificate([FromBody] LogInUserDto userDto)
        {
            var token = await _jwtAuthentificationManager.AuthentificateAsync(userDto.Login, userDto.Password);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
