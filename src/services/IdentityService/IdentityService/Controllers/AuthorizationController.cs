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
        private readonly ILogger<AuthorizationController> _logger;

        private readonly IJwtAuthentificationManager _jwtAuthentificationManager;

        private readonly IUserRepository _userRepository;
        public AuthorizationController(ILogger<AuthorizationController> logger, IUserRepository userRepository, 
            IJwtAuthentificationManager jwtAuthentificationManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _jwtAuthentificationManager = jwtAuthentificationManager;
        }

        //TODO add logger wrapper

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("login")]
        [HttpGet]
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
            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: register attempt");

            //TODO add automapper
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
                _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: register success");
                userDto.Id = user.Id;
                return CreatedAtAction(nameof(GetUser), new { id = user.Id}, userDto);
            }

            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: register failure : ${status.ToString()}");
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("bearer-token")]
        public async Task<ActionResult<string>> Authentificate([FromBody] LogInUserDto userDto)
        {
            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: authentication attempt");

            var token = await _jwtAuthentificationManager.AuthentificateAsync(userDto.Login, userDto.Password);

            if (token is null)
            {
                return Unauthorized();
            }

            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: successful authentication");

            return Ok(token);
        }




    }
}
