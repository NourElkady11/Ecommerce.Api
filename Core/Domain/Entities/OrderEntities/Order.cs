using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, AddressOfOrder shippingAddress, ICollection<OrderItems> orderItems, deliveryMethod deliveryWays, decimal subtotal, string paymentIntentId)
        {
            Id = Guid.NewGuid(); //it will be setet by default =>same number of digits but zeros
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            this.orderItems = orderItems;
            this.DeliveryWay = deliveryWays;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; }

        public AddressOfOrder ShippingAddress { get; set; }//mapped inside it its not relation between 2 entites

         public ICollection<OrderItems> orderItems { get; set; }= new List<OrderItems>(); //Relation (Collection)

        public OrderPymentStatus PymentStatus { get; set; } = OrderPymentStatus.pending;

        public deliveryMethod DeliveryWay { get; set; }//Relation (Navigational)
        public int? DelierywaysID { get; set; }

        public decimal Subtotal { get; set; }//Quantity * Price

        public string PaymentIntentId { get; set;}=string.Empty;


        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    }
}
