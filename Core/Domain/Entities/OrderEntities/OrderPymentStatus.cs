using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public enum OrderPymentStatus
    {
        pending=0,
        PaymentRecived=1,
        Paymentfailed = 2,
    }
}
