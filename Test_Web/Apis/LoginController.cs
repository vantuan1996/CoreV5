﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Library.Models;
using Test_Service.Admin;

namespace Test_Web.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _UserService;
        public LoginController(IUserService _UserService)
        {
            this._UserService = _UserService;
        }
        /// <summary>
        /// Api đăng nhập
        /// </summary>
        /// Author          Date            Summary
        /// TrungNQ         12/12/2019      Thêm mới tính năng
        /// <param name="value">{ Username, Password }</param>
        /// <returns>{ isSuccess, Message }</returns>
        [HttpPost]
        public async Task<MessageReport> Post([FromBody] AuthModel_LowSecurity value)
        {
            return await _UserService.SignIn(value);
        }

     
    }
}
