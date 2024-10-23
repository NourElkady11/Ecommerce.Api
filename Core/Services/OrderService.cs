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
        public async Task<OrderResult> CreateOrder(OrderRequest orderRequest, string UserEmail)
        {
            var address = mapper.Map<AddressOfOrder>(orderRequest.ShippingAddress);
            var backet = await backetRepository.GetcustomerBacketAsync(orderRequest.BascketId)??throw new BacketNotFound(orderRequest.BascketId);

            var orderItems = new List<OrderItems>();

            foreach (var item in backet.Items)
            {
               var prod = await unitOfWork.GetRepository<Product, int>().GetAsyncByid(item.Id) ?? throw new ProductNotFoundEx(item.Id);
               var oRderitem= CreateOrderItem(item, prod);
                orderItems.Add(oRderitem);
            }

            var DelvWays = await unitOfWork.GetRepository<DeliveryWays, int>().GetAsyncByid(orderRequest.DeliveryWayId) ?? throw new DeliveryWaysNotFound(orderRequest.DeliveryWayId);

            var subtot = orderItems.Sum(item => item.Price * item.Quantity);
            //Map to ==> AddressOfOrder From orderRequest.ShippingAddress
            var order =new Order(UserEmail,address, orderItems, DelvWays, subtot);
            await unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<OrderResult>(order);
        }

        private OrderItems CreateOrderItem(Backet_Item item, Product prod) => new OrderItems() { ProductName = prod.Name, ProductsId = prod.Id, PictureUrl = prod.PictureUrl, Price = prod.Price, Quantity = item.Quantity};

        public async Task<IEnumerable<DeliveryWaysResult>> GetDeliveryWaysAsync()
        {
            var DeliveryWays=await unitOfWork.GetRepository<DeliveryWays, int>().GetAllAsync();
            return DeliveryWays is null ? throw new Exception("DeliveryWays Not found") : mapper.Map<IEnumerable<DeliveryWaysResult>>(DeliveryWays);
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
