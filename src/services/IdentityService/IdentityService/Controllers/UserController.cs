using Domain.Entities;
using Domain.Enums;
using IdentityService.Controllers.Dtos;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
  
        [Route("{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserDto userDto)
        {

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
                userDto.Id = user.Id;
                return CreatedAtAction(nameof(GetUser), new { id = user.Id}, userDto);
            }

            return BadRequest();
        }


    }
}
