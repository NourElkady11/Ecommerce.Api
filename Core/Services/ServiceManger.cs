using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServiceManger : IServiceManger
    {
        private readonly Lazy<IProductService> productServicee;

        public ServiceManger(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.productServicee = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));

        }

        public IProductService productService => productServicee.Value;
    }
}
