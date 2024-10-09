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
        (!productSpecificationsParamters.BrandId.HasValue || productSpecificationsParamters.BrandId==product.BrandId)&&
        (!productSpecificationsParamters.TypeId.HasValue || productSpecificationsParamters.TypeId==product.TypeId) &&
        (string.IsNullOrWhiteSpace(productSpecificationsParamters.Search) || product.Name.ToLower().Contains(productSpecificationsParamters.Search.ToLower().Trim())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.productType);
            ApplyPagination(productSpecificationsParamters.PageIndex, productSpecificationsParamters.PageSize);
          

            if(productSpecificationsParamters.Sort is not null)
            {
                switch (productSpecificationsParamters.Sort)
                {
                    case productFilterations.NameAscending:
                        SetOrderBy(p => p.Name);
                        break;
                     case productFilterations.NameDescending:
                        SetOrderByDecending(p => p.Name);
                         break;
                     case productFilterations.PriceAscending:
                        SetOrderBy(p => p.Price);   
                        break;
                     case productFilterations.PriceDescending:
                        SetOrderByDecending(p => p.Price);
                        break;
                }
            }


           
        }


    }
}
