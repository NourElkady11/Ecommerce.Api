using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Shared;

namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllEelemntsAsync();
            var ProductResult = mapper.Map<IEnumerable<ProductDto>>(products);

            return ProductResult;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllEelemntsAsync();
            var brandResult = mapper.Map<IEnumerable<BrandDto>>(brands);
            //ba7awel men <IEnumerable<BrandDto>> from brands
            return brandResult;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllEelemntsAsync();
            var typesResult = mapper.Map<IEnumerable<TypeDto>>(types);

            return typesResult;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetElementByID(id);
            var ProductResult = mapper.Map<ProductDto>(product);
            return ProductResult;
        }
    }
}
