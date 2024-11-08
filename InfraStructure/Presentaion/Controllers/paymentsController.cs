using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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


      
        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await serviceManger.paymentService.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]);
            return new EmptyResult();
        
        }


    }
}
