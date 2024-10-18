using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class ServiceManger : IServiceManger
    {
        private readonly Lazy<IProductService> productServicee;
        private readonly Lazy<IBacketService> backetServicee;
        private readonly Lazy<IAuthenticationService> authenticationService;

        public ServiceManger(IUnitOfWork unitOfWork,IMapper mapper,IBacketRepository backetRepository, IAuthenticationService AuthenticationService,UserManager<User> userManager,IOptions<Jwtoptions> options)
        {
            this.productServicee = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
            this.backetServicee = new Lazy<IBacketService>(()=> new BacketService(backetRepository,mapper));
            this.authenticationService = new Lazy<IAuthenticationService>(()=> new AuthenticatinService(userManager,options));
            
        }

        public IProductService productService => productServicee.Value;

        public IBacketService backetService => backetServicee.Value;

        IAuthenticationService IServiceManger.authenticationService =>authenticationService.Value ;
    }
}
