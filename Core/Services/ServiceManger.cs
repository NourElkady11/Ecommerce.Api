using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
using Stripe;

namespace Services
{
    public sealed class ServiceManger(IUnitOfWork unitOfWork, IMapper mapper, IBacketRepository backetRepository, IAuthenticationService AuthenticationService, UserManager<User> userManager, IOptions<Jwtoptions> options, IConfiguration configuration, IChacheRepository cacheRepository) : IServiceManger
    {
        private readonly Lazy<IProductService> productServicee=new(()=>new ProductService(unitOfWork,mapper));
        private readonly Lazy<IBacketService> backetServicee=new(()=>new BacketService(backetRepository,mapper));
        private readonly Lazy<IAuthenticationService> authenticationServiceeee=new(()=>new AuthenticatinService(userManager,options,mapper));
        private readonly Lazy<IOrderService> orderServicee=new(()=>new OrderService(unitOfWork,mapper,backetRepository));
        private readonly Lazy<IPaymentService> paymentServiceee=new(()=>new PaymentService(backetRepository,unitOfWork,mapper,configuration));
        private readonly Lazy<ICachService> cachServiceee=new(()=>new CachService(cacheRepository));


   

        public IProductService productService => productServicee.Value;

        public IBacketService backetService => backetServicee.Value;

        public IOrderService orderService =>orderServicee.Value;

        public IPaymentService paymentService => paymentServiceee.Value;

        public ICachService cachService => cachServiceee.Value;

        IAuthenticationService IServiceManger.authenticationService => authenticationServiceeee.Value ;
    }
}                                                                 
