using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class paymentsController(IServiceManger serviceManger):ApiController
    {

        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>>CreateOrUpdatePaymentIntent(string basketId)
        {
            var result=await serviceManger.paymentService.CreateUpdatePaymentIntentAsync(basketId);
            return Ok(result);
        }


    }
}
