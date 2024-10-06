using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specfications;
using Shared;

namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort ,int?brandId , int? typeId)
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWhithBrandAndTypeSpecfications(sort,brandId,typeId));
            var ProductResult = mapper.Map<IEnumerable<ProductDto>>(products);

            return ProductResult;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(new ProductWhithBrandAndTypeSpecfications(id));
            var ProductResult = mapper.Map<ProductDto>(product);
            return ProductResult;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandResult = mapper.Map<IEnumerable<BrandDto>>(brands);
            //ba7awel men <IEnumerable<BrandDto>> from brands
            return brandResult;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesResult = mapper.Map<IEnumerable<TypeDto>>(types);

            return typesResult;
        }

    }
}
