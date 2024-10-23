using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AddressDto
    {
        public string Username { get; set; }//to specify the name of the user who want to order 
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
     
    }
}
