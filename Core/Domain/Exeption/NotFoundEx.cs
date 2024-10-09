using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption
{
    public abstract class NotFoundEx : Exception
    {
        public NotFoundEx(string message):base(message)
        {

        }
    }
}
