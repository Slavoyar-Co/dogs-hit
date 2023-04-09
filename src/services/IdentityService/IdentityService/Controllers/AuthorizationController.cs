using Domain.Entities;
using Domain.Enums;

using IdentityService.Controllers.Dtos;
using IdentityService.Services;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Oauth2.v2;
using IdentityService.Services.Models;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IGoogleAuthManager _googleAuthService;

        private readonly IUserRepository _userRepository;
        public AuthorizationController(IAuthenticationService authenticationService,
            IGoogleAuthManager googleAuthService, IUserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _googleAuthService = googleAuthService;
            _userRepository = userRepository;
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

            var status = await _userRepository.CreateUserAsync(user);

            if (status == ERegistrationStatus.Success)
            {
                userDto.Id = user.Id;
                return CreatedAtAction(nameof(GetUser), new { id = user.Id}, userDto);
            }

            return BadRequest();
        }
         
        [AllowAnonymous]
        [HttpGet("bearer-token")]
        public async Task<ActionResult<string>> Authentificate([FromBody] LogInUserDto userDto)
        {
            var token = await _authenticationService.AuthentificateByCredentialsAsync(userDto.Login, userDto.Password);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }


        [AllowAnonymous]
        [HttpGet("google")]
        public async Task<ActionResult<string>> SignInWithGoogle([FromQuery] GoogleSignInModel userModel)
        {

            var tokenResponse = await _googleAuthService.GetFlowToken(userModel);

            var token = await _authenticationService.AuthenticateViaGoogleAsync(tokenResponse.IdToken);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}
