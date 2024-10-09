using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption
{
    public class ProductNotFoundEx : NotFoundEx
    {
        public ProductNotFoundEx(int id) : base($"Product with id {id} is Not Found")
        {

        }
    }
}
