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

        public List<OrderItemsDto> orderItems { get; set; } //Relation (Collection)

        public string PymentStatus { get; set; }

        public string DeliveryWays { get; set; }//Relation (Navigational)

        public decimal Subtotal { get; set; }//Quantity * Price

        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public DateTimeOffset dateTimeOffset { get; set; } = DateTimeOffset.Now;
    }
}
