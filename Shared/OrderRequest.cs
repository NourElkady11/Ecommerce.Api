using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderRequest
    {
        public string BascketId { get; set; }

        public AddressDto ShippingAddress { get; set; }

        public int DeliveryWayId { get; set; }
    }
}
