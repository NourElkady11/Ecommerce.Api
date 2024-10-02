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
    public class ProductProfile:Profile
    {
        public ProductProfile() {

             CreateMap<Product, ProductDto>().ForMember(d=>d.BrandName,options=>options.MapFrom(s=>s.ProductBrand.Name)).ForMember(d=>d.TypeName,op=>op.MapFrom(s=>s.productType.Name)).ForMember(s=>s.PictureUrl,op=>op.MapFrom<PictureUrlResolver>());
             CreateMap<ProductBrand, BrandDto>();
             CreateMap<ProductType, TypeDto>();


        
        }
    }
}
