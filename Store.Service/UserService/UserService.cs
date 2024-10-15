using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntity;
using Store.Service.TokenServices;
using Store.Service.UserService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenServices tokenServices;

        public UserService(UserManager<AppUser> userManager ,SignInManager<AppUser> signInManager , ITokenServices  tokenServices)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
           var user=await userManager.FindByEmailAsync(input.Email);
            if (user is null)
                return null;
            var result = await signInManager.CheckPasswordSignInAsync(user, input.Password, false);
            if (!result.Succeeded)
                throw new Exception("Login Failed");
            return new UserDto
            {
                Email = input.Email,
                DisplayName = user.DisplayName,
                Token = tokenServices.GenerateToken(user)

            };
        }   

        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await userManager.FindByEmailAsync(input.Email);
            if (user is not null)
                return null;
            var appuser = new AppUser
            {
                DisplayName = input.DisplayName,
                UserName = input.DisplayName,
                Email = input.Email,

            };
            var result = await userManager.CreateAsync( appuser,input.Password);
            if (!result.Succeeded)
                throw new Exception(result.Errors.Select(x=>x.Description).FirstOrDefault());
            return new UserDto
            {
                Email = input.Email,
                DisplayName = input.DisplayName,
                Token = tokenServices.GenerateToken(appuser)

            };
        }
    }
}
