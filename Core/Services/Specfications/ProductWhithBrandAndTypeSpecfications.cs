using AutoMapper.Configuration;
using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    public class ProductWhithBrandAndTypeSpecfications : Specifications<Product>
    {
        //Using to retrive product by id
        public ProductWhithBrandAndTypeSpecfications(int id):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.productType);
        }

        //get all products

        public ProductWhithBrandAndTypeSpecfications(ProductSpecificationsParamters productSpecificationsParamters):base(product=>
        (!productSpecificationsParamters.BrandId.HasValue || productSpecificationsParamters.BrandId==product.brandId)&&
        (!productSpecificationsParamters.TypeId.HasValue || productSpecificationsParamters.TypeId==product.typeId) &&
        (string.IsNullOrWhiteSpace(productSpecificationsParamters.Search) || product.name.ToLower().Contains(productSpecificationsParamters.Search.ToLower().Trim())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.productType);
            ApplyPagination(productSpecificationsParamters.PageIndex, productSpecificationsParamters.PageSize);
          

            if(productSpecificationsParamters.Sort is not null)
            {
                switch (productSpecificationsParamters.Sort)
                {
                    case productFilterations.nameAsc:
                        SetOrderBy(p => p.name);
                        break;
                     case productFilterations.nameDsc:
                        SetOrderByDecending(p => p.name);
                         break;
                     case productFilterations.priceAsc:
                        SetOrderBy(p => p.price);   
                        break;
                     case productFilterations.priceDsc:
                        SetOrderByDecending(p => p.price);
                        break;
                }
            }


           
        }


    }
}
