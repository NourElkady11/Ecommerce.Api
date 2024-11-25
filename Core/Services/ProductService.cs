using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exeption;
using Services.Abstractions;
using Services.Specfications;
using Shared;

namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductSpecificationsParamters productSpecificationsParamters)
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWhithBrandAndTypeSpecfications(productSpecificationsParamters));
            var ProductResult = mapper.Map<IEnumerable<ProductDto>>(products);
            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(new ProductWhithBrandAndTypeSpecfications(productSpecificationsParamters));
            var result = new PaginatedResult<ProductDto>(productSpecificationsParamters.PageIndex, ProductResult.Count(), totalCount, ProductResult);
            return result;

        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(new ProductWhithBrandAndTypeSpecfications(id));
            return product is null ? throw new ProductNotFoundEx(id) : mapper.Map<ProductDto>(product);
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
