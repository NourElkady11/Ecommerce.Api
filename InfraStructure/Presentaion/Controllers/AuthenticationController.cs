using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class AuthenticationController(IServiceManger serviceManger):ApiController
    {


        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>>Login(LoginDto loginDto)
        {
            var result=await serviceManger.authenticationService.LoginAsync(loginDto);
            return Ok(result);
        }



        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register(UserRegisterDto userRegisterDto)
        {
            var result = await serviceManger.authenticationService.RegisterAsync(userRegisterDto);
            return Ok(result);
        }



    }
}
