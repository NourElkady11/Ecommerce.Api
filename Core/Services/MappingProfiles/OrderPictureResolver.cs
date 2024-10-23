using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderPictureResolver(IConfiguration configuration) : IValueResolver<OrderItems, OrderItemsDto, string>
    {
        public string Resolve(OrderItems source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
            }
        }
    }
}
