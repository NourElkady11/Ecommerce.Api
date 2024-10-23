using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResult>().ForMember(o=>o.PymentStatus,k=>k.MapFrom(s=>s.PymentStatus.ToString())).
                ForMember(o => o.DeliveryWays, k => k.MapFrom(s =>s.DeliveryWay.ShortName))
                .ForMember(o=>o.TotalPrice,s=>s.MapFrom(d=>d.Subtotal+d.DeliveryWay.Cost));
            CreateMap<AddressOfOrder, AddressDto>().ReverseMap();
            CreateMap<OrderItems, OrderItemsDto>().ForMember(s=>s.PictureUrl,s=>s.MapFrom<OrderPictureResolver>());
            CreateMap<DeliveryWays, DeliveryWaysResult>();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}