using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        public Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductSpecificationsParamters productSpecificationsParamters);

        public Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

        public Task<IEnumerable<TypeDto>> GetAllTypesAsync();

        public Task<ProductDto> GetProductById(int id);

    }
}
