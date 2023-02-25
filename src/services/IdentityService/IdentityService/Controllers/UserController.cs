using Domain.Entities;
using Domain.Enums;
using IdentityService.Controllers.Dtos;
using IdentityService.Services;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IJwtAuthentificationManager _jwtAuthentificationManager;

        private readonly IUserRepository _userRepository;
        public UserController(ILogger<UserController> logger, IUserRepository userRepository, 
            IJwtAuthentificationManager jwtAuthentificationManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _jwtAuthentificationManager = jwtAuthentificationManager;
        }

        [Route("{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: GET user attempt");

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: user not found");
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: register attempt");

            //TODO add automapper
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = userDto.Login,
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

            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: register failure");
            return BadRequest();
        }

        [HttpPost("bearer-token")]
        public async Task<ActionResult<string>> Authentificate([FromBody] UserDto userDto)
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
