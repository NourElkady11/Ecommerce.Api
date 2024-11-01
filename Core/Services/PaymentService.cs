global using Product = Domain.Entities.Product;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Exeption;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Shared;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class PaymentService(IBacketRepository backetRepository,IUnitOfWork unitOfWork,IMapper mapper,IConfiguration configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateUpdatePaymentIntentAsync(string backetId)
        {
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("StripeSettings")["SecretKey"];
            //first get your secret key from Stripe
            var backet=await backetRepository.GetcustomerBacketAsync(backetId)??throw new BacketNotFound(backetId);

            foreach (var item in backet.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsyncByid(item.Id);
                item.price = product.price;
            }

            if (!backet.deliveryMethodId.HasValue)
            {
                throw new Exception("No Delivery Method Is Selected");
            }
            else
            {
                var way = await unitOfWork.GetRepository<deliveryMethod, int>().GetAsyncByid(backet.deliveryMethodId.Value) ?? throw new DeliveryWaysNotFound(backet.deliveryMethodId.Value);
                backet.shippingPrice=way.cost;

            }
            var amount = (long)(backet.Items.Sum(item => item.quantity * item.price) + backet.shippingPrice) * 100;
            var servicePayment=new PaymentIntentService();
            if (string.IsNullOrWhiteSpace(backet.paymentIntentId))
            {
                //Create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> {"card"}
                };
                var paymentintent = await servicePayment.CreateAsync(options);
                backet.paymentIntentId = paymentintent.Id;
                backet.clientSecret = paymentintent.ClientSecret;
            }
            else
            {
                //update in case he remove some items from the backet instead od creating new paymentIntent we can update it with the new amount
                var updateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = amount,

                };
                await servicePayment.UpdateAsync(backet.paymentIntentId, updateOptions);
            }

            await backetRepository.CreateOrUpdateBacketAsync(backet);
            return mapper.Map<BasketDto>(backet);

          
        }
    }
}
