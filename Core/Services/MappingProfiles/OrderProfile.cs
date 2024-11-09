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
            CreateMap<OrderItems, OrderItemsDto>().ForMember(s => s.PictureUrl, s => s.MapFrom<OrderPictureResolver>());
            CreateMap<Order, OrderResult>().ForMember(o => o.status, k => k.MapFrom(s => s.PymentStatus.ToString())).
                ForMember(o => o.DeliveryWays, k => k.MapFrom(s => s.DeliveryWay.shortName)).ForMember(o => o.orderDate, o => o.MapFrom(s => s.OrderDate))
                .ForMember(o => o.total, s => s.MapFrom(d => d.Subtotal + d.DeliveryWay.cost)).ReverseMap();
            CreateMap<AddressOfOrder, AddressDto>().ReverseMap();
            CreateMap<deliveryMethod, deliveryMethodResult>();
            CreateMap<AddressDto, Address>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.firstname} {src.lastname}")).ReverseMap();
            CreateMap<AddressDto, AddressOfOrder>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.firstname} {src.lastname}")).ReverseMap();
        }
    }
}