using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities.IdentityEntity;
using Store.Service.HandleResponses;
using Store.Service.TokenServices;
using Store.Service.UserService;
using Store.Service.UserService.DTOS;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace Store.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<AppUser> userManager;

        public AccountController(IUserService userService , UserManager<AppUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var  user= await userService.Login(input);
            if (user is null)
                return Unauthorized(new CustomException(401));
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await userService.Register(input);
            if (user is null)
                return BadRequest(new CustomException(400,"Email Already Exists"));
            return Ok(user);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCUrrentUserDetails()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user=await userManager.FindByEmailAsync(email);
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
             

            };

        }
    }
}
