using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Jwtoptions
    {
        public int DurationInDays { get; set; }
        public string Audiance { get; set; }
        public string Issuer { get; set; }
        public string MySecretKey { get; set; }
    }
}
