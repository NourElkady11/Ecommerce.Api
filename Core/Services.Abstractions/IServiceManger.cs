using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManger
    {
        public IProductService productService { get; }
        public IBacketService backetService { get; }
        public IAuthenticationService authenticationService { get; }
        public IOrderService orderService { get; }
       public IPaymentService paymentService { get; }

    }
}
