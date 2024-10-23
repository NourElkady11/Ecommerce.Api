using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption
{
    public class UserNotFoundException(string email):Exception($"Email {email} was not Found")
    {
    }
}
