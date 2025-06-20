﻿using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateUpdatePaymentIntentAsync(string backetId);
        public Task UpdateOrderPaymentStatus(string Jsonrequest, string stripeHeader);
    }
}
