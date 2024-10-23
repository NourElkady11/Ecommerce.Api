using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var result = await serviceManger.authenticationService.CheckEmailExist(email);
            return Ok(result);
        }



        [HttpGet("user")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.authenticationService.GetUserByEMail(email);
            return Ok(result);
        }




        [HttpGet("Address")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await serviceManger.authenticationService.GetUserAdddress(email);
            return Ok(address);
        }



        [HttpPut("Address")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await serviceManger.authenticationService.UpdateUserAdddress(addressDto,email);
            return Ok(address);
        }






    }
}
