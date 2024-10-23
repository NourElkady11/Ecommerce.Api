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
        private readonly Lazy<IAuthenticationService> authenticationServiceeee;
        private readonly Lazy<IOrderService> orderServicee;


        public ServiceManger(IUnitOfWork unitOfWork,IMapper mapper,IBacketRepository backetRepository, IAuthenticationService AuthenticationService,UserManager<User> userManager,IOptions<Jwtoptions> options)
        {
            this.productServicee = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
            this.backetServicee = new Lazy<IBacketService>(()=> new BacketService(backetRepository,mapper));
            this.authenticationServiceeee = new Lazy<IAuthenticationService>(()=> new AuthenticatinService(userManager,options,mapper));
            this.orderServicee = new Lazy<IOrderService>(()=> new OrderService(unitOfWork,mapper,backetRepository));
            
        }

        public IProductService productService => productServicee.Value;

        public IBacketService backetService => backetServicee.Value;

        public IOrderService orderService =>orderServicee.Value;

        IAuthenticationService IServiceManger.authenticationService => authenticationServiceeee.Value ;
    }
}                                                                 
