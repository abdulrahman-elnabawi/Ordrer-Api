using Core.IdentityEntities;
using Demo.HandelResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Services.TokenService;
using Services.UserService;
using Services.UserService.Dto;
using System.Security.Claims;

namespace Demo.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService, UserManager<AppUser> userManager)
        {
            this._userService = userService;
            this._userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.Register(registerDto);

            

            if (user == null)
                return BadRequest(new ApiException(400, "Email Already Exist"));

            return Ok(user);
            
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto  loginDto)
        {
            var user = await _userService.Login(loginDto);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            return Ok(user);
        }
        [HttpGet("getCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Email).Value;
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email
            };

        }


    }
}
