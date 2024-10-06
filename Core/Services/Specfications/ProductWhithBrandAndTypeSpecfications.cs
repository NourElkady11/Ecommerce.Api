using AutoMapper.Configuration;
using Domain.Contracts;
using Domain.Entities;
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
        public ProductWhithBrandAndTypeSpecfications(string? sort ,int? brandid ,int? TypeId) : base(product=>(!brandid.HasValue || product.BrandId==brandid.Value )&&(!TypeId.HasValue || product.TypeId==TypeId.Value))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.productType);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim())
                {
                    case "pricedescending":
                        SetOrderByDecending(p => p.Price);
                        break;
                    case "priceascending":
                        SetOrderBy(p => p.Price);
                        break;
                    case "namedescending":
                        SetOrderByDecending(p => p.Name);
                        break;
                    case "nameascending":
                        SetOrderBy(p => p.Name);
                        break; 

                        default:
                        break;
                }
            }
        }
    }
}
