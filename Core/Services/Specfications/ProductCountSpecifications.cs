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
        (!productSpecificationsParamters.BrandId.HasValue || productSpecificationsParamters.BrandId == product.BrandId) &&
        (!productSpecificationsParamters.TypeId.HasValue || productSpecificationsParamters.TypeId == product.TypeId) &&
        (string.IsNullOrWhiteSpace(productSpecificationsParamters.Search) || product.Name.ToLower().Contains(productSpecificationsParamters.Search.ToLower().Trim())))
        {
 
        }

     
    }
}
