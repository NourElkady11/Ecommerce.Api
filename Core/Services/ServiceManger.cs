using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServiceManger : IServiceManger
    {
        private readonly Lazy<IProductService> productServicee;
        private readonly Lazy<IBacketService> backetServicee;

        public ServiceManger(IUnitOfWork unitOfWork,IMapper mapper,IBacketRepository backetRepository)
        {
            this.productServicee = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
            this.backetServicee = new Lazy<IBacketService>(()=> new BacketService(backetRepository,mapper));
            
        }

        public IProductService productService => productServicee.Value;

        public IBacketService backetService => backetServicee.Value;
    }
}
