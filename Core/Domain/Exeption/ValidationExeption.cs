using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption
{
    public class ValidationExeption:Exception
    {
        
        public IEnumerable<string> Errors { get; set; }

        public ValidationExeption(IEnumerable<string> errors):base("Validation Failed")
        {
            this.Errors= errors;
        }



    }
}
