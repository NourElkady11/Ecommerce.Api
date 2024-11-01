using AutoMapper;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class BacketProfile:Profile
    {
        public BacketProfile()
        {
            CreateMap<CustomerBacket, BasketDto>().ReverseMap();
            CreateMap<Basket_Item, Basket_ItemsDto>().ReverseMap();
        }
    }
}
