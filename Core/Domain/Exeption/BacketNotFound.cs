using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption
{
    public sealed class BacketNotFound(string id):NotFoundEx($"The backet with id {id} is not Found")
    {

    }
}
