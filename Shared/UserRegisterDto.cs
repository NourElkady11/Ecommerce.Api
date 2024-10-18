using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UserRegisterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage="Username is required")]
        public string Username { get; set; } 

        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

      /*  [RegularExpression("")]*/

        [Required(ErrorMessage="password is required")]
        public string password { get; set; }  

        [Required(ErrorMessage="Username is required")]
        public string? phoneNumber { get; set; }
    }
}
