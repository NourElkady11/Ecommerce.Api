using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specfications
{
    public class ProductCountSpecifications : Specifications<Product>
    {
        public ProductCountSpecifications(ProductSpecificationsParamters productSpecificationsParamters) : base(product =>
        (!productSpecificationsParamters.BrandId.HasValue || productSpecificationsParamters.BrandId == product.brandId) &&
        (!productSpecificationsParamters.TypeId.HasValue || productSpecificationsParamters.TypeId == product.typeId) &&
        (string.IsNullOrWhiteSpace(productSpecificationsParamters.Search) || product.name.ToLower().Contains(productSpecificationsParamters.Search.ToLower().Trim())))
        {
 
        }

     
    }
}
