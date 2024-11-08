using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exeption;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Specfications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IUnitOfWork unitOfWork,IMapper mapper,IBacketRepository backetRepository) : IOrderService
    {
        public async Task<OrderResult> CreateOrUpdateOrder(OrderRequest orderRequest, string UserEmail)
        {
            var address = mapper.Map<AddressOfOrder>(orderRequest.shipToAddress);
            var backet = await backetRepository.GetcustomerBacketAsync(orderRequest.BasketId)??throw new BacketNotFound(orderRequest.BasketId);
           
            var orderItems = new List<OrderItems>();

            foreach (var item in backet.Items)
            {
               var prod = await unitOfWork.GetRepository<Product, int>().GetAsyncByid(item.Id) ?? throw new ProductNotFoundEx(item.Id);
               var oRderitem= CreateOrderItem(item, prod);
                orderItems.Add(oRderitem);
            }
            var order_Repo = unitOfWork.GetRepository<Order, Guid>();
            var existingOrder =await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderWithPaymentIntentSpecefications(backet.paymentIntentId));
            if(existingOrder is not null)
            {
                order_Repo.DeleteAsync(existingOrder);
            }

            var DelvWays = await unitOfWork.GetRepository<deliveryMethod, int>().GetAsyncByid(orderRequest.deliveryMethodId) ?? throw new DeliveryWaysNotFound(orderRequest.deliveryMethodId);

            var subtot = orderItems.Sum(item => item.Price * item.Quantity);
            //Map to ==> AddressOfOrder From orderRequest.ShippingAddress
            var order =new Order(UserEmail,address, orderItems, DelvWays, subtot,backet.paymentIntentId);
            await unitOfWork.GetRepository<Order,Guid>().AddAsync(order);

            await unitOfWork.SaveChangesAsync();
            return mapper.Map<OrderResult>(order);

        }

        private OrderItems CreateOrderItem(Basket_Item item, Product prod) => new OrderItems() { ProductName = prod.name, ProductsId = prod.Id, PictureUrl = prod.pictureUrl, Price = prod.price, Quantity = item.quantity.Value};

        public async Task<IEnumerable<deliveryMethodResult>> GetDeliveryWaysAsync()
        {
            var DeliveryMethods=await unitOfWork.GetRepository<deliveryMethod, int>().GetAllAsync();
            return DeliveryMethods is null ? throw new Exception("DeliveryWays Not found") : mapper.Map<IEnumerable<deliveryMethodResult>>(DeliveryMethods);
        }

        public async Task<OrderResult> GetOrderbyIdAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderWithIncludeSpecficarions(id))??throw new OrderNotFoundException(id);
            return mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersbyEmailAsync(string email)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderWithIncludeSpecficarions(email));
            return mapper.Map<IEnumerable<OrderResult>>(order);
        }
    }
}
