using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderResult
    {
        public Guid Id { get; set; }

        public string UserEmail { get; set; }

        public AddressDto ShippingAddress { get; set; }//mapped inside it its not relation between 2 entites

        public List<OrderItemsDto> items { get; set; } //Relation (Collection)

        public string status { get; set; }

        public string DeliveryWays { get; set; }//Relation (Navigational)

        public decimal subtotal { get; set; }//Quantity * Price

        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal total { get; set; }

        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;
    }

}
