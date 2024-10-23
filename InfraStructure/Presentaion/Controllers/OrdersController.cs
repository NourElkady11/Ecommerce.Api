using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class OrdersController(IServiceManger serviceManger):ApiController
    {
     
        [HttpGet("DeliveryWays")]
        [AllowAnonymous]
        public async Task<ActionResult<DeliveryWaysResult>> GetAllDeliveryWays()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManger.orderService.GetDeliveryWaysAsync();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManger.orderService.CreateOrder(request, email);
            return Ok(order);

        }


        [HttpGet]
        public async Task<ActionResult<OrderResult>> GetOrdersByEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManger.orderService.GetOrdersbyEmailAsync(email);
            return Ok(order);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrdersByid(Guid id)
        {
            var order = await serviceManger.orderService.GetOrderbyIdAsync(id);
            return Ok(order);

        }





    }
}
