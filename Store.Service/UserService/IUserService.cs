﻿using Store.Service.UserService.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.UserService
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto input);
        Task<UserDto> Login(LoginDto input);
    }
}
