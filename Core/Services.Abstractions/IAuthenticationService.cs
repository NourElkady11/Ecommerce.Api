using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> LoginAsync(LoginDto loginDto);

        public Task<UserResultDto> RegisterAsync(UserRegisterDto RegisterDto);

        public Task<UserResultDto> GetUserByEMail(string email);

        public Task<bool> CheckEmailExist(string email);


        public Task<AddressDto> GetUserAdddress(string email);


        public Task<AddressDto> UpdateUserAdddress(AddressDto addressDto,string email);


    }
}
