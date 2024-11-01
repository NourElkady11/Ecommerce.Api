using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    public class OrderWithPaymentIntentSpecefications : Specifications<Order>
    {
        public OrderWithPaymentIntentSpecefications(string paymentid) : base(order=>order.PaymentIntentId==paymentid)
        {

        }
    }
}
