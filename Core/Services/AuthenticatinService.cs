using Domain.Entities;
using Domain.Exeption;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticatinService(UserManager<User> userManager,IOptions<Jwtoptions> options) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                throw new UnAuthorizedException("This Email Dosent Exist");
            }
            else
            {
                var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!result)
                {
                    throw new UnAuthorizedException("Invalid Email or Password");
                }
                else
                {
                    var userDto = new UserResultDto(user.UserName, user.Email,await CreateTokenAsync(user));
                    return userDto;
                }
            }
        }

        public async Task<UserResultDto> RegisterAsync(UserRegisterDto RegisterDto)
        {
           
            var user = new User()
            {
                Email = RegisterDto.Email,
                FirstName = RegisterDto.FirstName,
                LastName = RegisterDto.LastName,
                UserName = RegisterDto.Username,
                PhoneNumber = RegisterDto.phoneNumber

            };
            var result = await userManager.CreateAsync(user, RegisterDto.password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationExeption(errors);

            }
            else
            {
                var userDto = new UserResultDto(user.UserName, user.Email, await CreateTokenAsync(user));
                return userDto;
            }
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = options.Value;
            var authClaims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.MySecretKey));

            var mySigningCredintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: authClaims,
                /////////////////////////////
                audience:jwtOptions.Audiance,
                issuer:jwtOptions.Issuer,
                expires:DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials:mySigningCredintials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);



        }
    }
}

