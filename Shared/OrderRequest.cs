using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderRequest
    {
        public string BasketId { get; set; }

        public AddressDto shipToAddress { get; set; }

        public int deliveryMethodId { get; set; }
    }
}
