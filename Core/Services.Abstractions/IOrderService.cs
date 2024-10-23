using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        public Task<OrderResult> GetOrderbyIdAsync(Guid id);

        public Task<IEnumerable<OrderResult>> GetOrdersbyEmailAsync(string email);

        public Task<OrderResult> CreateOrder(OrderRequest orderRequest, string UserEmail);

        public Task<IEnumerable<DeliveryWaysResult>> GetDeliveryWaysAsync(); 


    }
}
